using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//Movement using new input system
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerControls;
    public Animator animator;

    [Header("General")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wall;

    //Basic movement
    [Header("Speeds")]
    private float horizontalInput;
    public float speed = 8f;
    public float jumpingSpeed = 16f;
    public bool isFacingRight = true;

    //Double jump
    private bool doubleJump;

    //Dashing
    [Header("Dash")]
    private bool dashAllowed = true;
    private bool isDashing;
    public float dashSpeed = 24f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1.5f;
    public float dashTrailTime = 1f;

    //Wall sliding
    private bool isWallSliding;
    public float wallSlidingSpeed = 4f;

    //Increasing gravity while falling so we fall faster
    private float originalGravity;
    public float gravityLimit; //Max value gravity can be while falling
    private float lastY; //Last Y axis value to compare to to check if we are falling
    private float gravityStep;

    //Wall jumping
    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpTime = 0.2f;
    private float wallJumpTimer;
    public float wallJumpDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(2f, 8f);

    //Sound and Visual Effects
    //READ ME: SFX have been disabled as they weren't configured in my scene and it was breaking movement
    //so ive temporarily commented them out. 
    [Header("Sound and Visual Effects")]
    public ParticleSystem jumpVFX;
    public ParticleSystem dashVFX;
    public ParticleSystem landGroundVFX;
    public ParticleSystem landWallVFX;
    public ParticleSystem dashRechargeVFX;
    TrailRenderer dashTrail;
    public AudioSource jumpSFX;
    public AudioSource dashSFX;
    public AudioSource dashCooldownFinished;
    public AudioSource landSFX;
    public AudioSource wallJumpSFX;
    public AudioSource timeSwapSFX;
    public GameObject timeSwapVFX;

    private InputAction horiz;
    private InputAction jump;
    private InputAction dashed;
    private InputAction prsnt;
    private InputAction futr;

    //Time Switcher Values
    /* Ive moved that script here as getting it to interact with the input system being created in this script was a pain
     * Im fully aware this is a messy way of doing it but this is going to work */
    [Header("Time Switcher")]
    public bool inFuture = false;
    public float switchCooldown = 2f;
    private float timer;
    [SerializeField] bool oneClickOption = false;
    [SerializeField] GameObject playerCam;

    //UI animation
    [SerializeField] GameObject presentCogs;
    [SerializeField] GameObject futureCogs;

    public Animator animatorPresent;
    public Animator animatorPresentTwo;
    public Animator animatorFuture;
    public Animator animatorFutureTwo;


    private void Awake()
    {
        playerControls = new PlayerInput();
    }

    private void OnEnable()
    {
        //Sets each variable to a particluar action the player can complete
        //We can use this later to check for events
        horiz = playerControls.Player.LeftRight;
        jump = playerControls.Player.Jump;
        dashed = playerControls.Player.Dash;
        prsnt = playerControls.Player.PresentSwitch;
        futr = playerControls.Player.Future;
        playerControls.Enable();
    }

    //Disable player input :)
    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        //Store original gravty and how much to increase it by for every frame we're in the air
        originalGravity = rb.gravityScale;
        gravityStep = gravityLimit / 120;
        //Get the trail renderer component and disable it so trail isnt always showing
        dashTrail = GetComponent<TrailRenderer>();
        dashTrail.enabled = false;
        timeSwapVFX.SetActive(false);
    }

    void Update()
    {

        animator.SetBool("AniIsFalling", !(isGrounded() && rb.velocity.y <= 0));
        //All time swithching code 
        //We assume teleporting method, old method can be found in legacy/now unused script

        timeSwitch();

        //Prevents player inputting extra actions while dashing
        if(isDashing)
        {
            return;
        }

        horizontalInput = horiz.ReadValue<float>();
        animator.SetFloat("AniSpeed", Mathf.Abs(horizontalInput));

        //This code is pointless :)
        //Jumping
        if (isGrounded() && jump.WasPressedThisFrame()) //First Jump
        {
            Debug.Log("grounded jump this frame");
            jumpVFX.Play();
            jumpSFX.Play();
            animator.SetBool("AniIsJumping",true);
            animator.SetTrigger("CanJumpTrigger");
            doubleJump = false;
        }

        if(jump.WasPressedThisFrame() && (isGrounded() || doubleJump)) //Double Jump
        {
            Debug.Log("grounded jump this frame or double jump"); 
            jumpVFX.Play();
            jumpSFX.Play();
            animator.SetBool("AniIsJumping", true);
            animator.SetTrigger("CanJumpTrigger");
            rb.velocity = new Vector2(rb.velocity.x, jumpingSpeed);
            doubleJump = !doubleJump;
        }
        
        if (jump.WasPressedThisFrame() && rb.velocity.y > 0.0f) //Shortened Jump
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        //Dash Code
        if (dashed.WasPressedThisFrame() && dashAllowed)
        {
            StartCoroutine(dash());
        }

        wallSlide();
        wallJump();

        //Stops player fliping while wall jumping 
        if(!isWallJumping)
        {
            flipPlayer();
        }
        
    }

    private void FixedUpdate()
    {

        
        //Increase gravity on falling
        if (transform.position.y < lastY)
        {
            //Set anim bool AniIsJumping to false when we're falling
            animator.SetBool("AniIsFalling", true);
            if (rb.gravityScale < gravityLimit)
            {
                rb.gravityScale += gravityStep;
            }
        }
        //Reset gravity when not falling
        else
        {
            rb.gravityScale = originalGravity;
        }
        lastY = transform.position.y;

        //Prevents player inputting extra actions while dashing
        if (isDashing)
        {
            return;
        }

        //Takes horizontal input from update and sets speed if we are not wall jumping
        if(!isWallJumping)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }
        
    }

    private bool isGrounded()
    {
        //Used a overlap circle on the groundCheck game object to see if we are grounded
        bool result = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
        if (result)
        {
            animator.SetBool("AniIsFalling", false);
            animator.SetBool("AniIsJumping", false);
        }
        return result;
    }

    private bool isOnWall()
    {
        bool result = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wall);
        if (result)
        {
         
        }
        return result;
    }

    private void wallSlide()
    {
        //If is wall sliding, set velocity to (velocity.x, and clamp Y between its current velocity and wallslide speed
        //This means we dont continue accelerating down the wall
        if (isOnWall() && !isGrounded() && horizontalInput != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
            
    }

    private void wallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x; //We want to wall jump in the opposite direction we are facing
            wallJumpTimer = wallJumpTime;
            CancelInvoke(nameof(stopWallJump));
        }
        //Lets us leave the wall and be able to wall jump for a brief moment of time
        else
        {
            wallJumpTimer -= Time.deltaTime;
        }

        if (jump.WasPressedThisFrame() && wallJumpTimer > 0f)
        {
            wallJumpSFX.Play();
            isWallJumping = true;
        
            rb.velocity = new Vector2(wallJumpDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpTimer = 0f;
            //Flips player if facing direction and wallJumpDirection arent equal
            if (transform.localScale.x != wallJumpDirection)
            {
                dashVFX.transform.Rotate(0, 180, 0); //Invert dash effect to face correctly
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }

            Invoke(nameof(stopWallJump), wallJumpDuration); //Calls function stopWallJump after duration of wallJump
        }
    }
    private void stopWallJump()
    {
        isWallJumping = false;
    }

    private void flipPlayer()
    {
        //Based on horizontal input, switch isFacingRight
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f) 
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            dashVFX.transform.Rotate(0, 180, 0); //Invert dash effect to face correctly
        }
    }

    private IEnumerator dash()
    {
        dashAllowed = false;
        isDashing = true;
        float startingGravity = rb.gravityScale;
        rb.gravityScale = 0; //Sets gravity to 0 so the player doesnt fall during the dash
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f ); //transform.localScale.x is player direction
        dashVFX.Play();
        dashSFX.Play();
        StartCoroutine(dashTrailFunction());
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = startingGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        dashAllowed = true;
        dashCooldownFinished.Play();
        dashRechargeVFX.Play();
    }

    //Function to enable and disable dash trailrenderer
    private IEnumerator dashTrailFunction()
    {
        dashTrail.enabled = true;
        yield return new WaitForSeconds(dashTrailTime);
        dashTrail.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            landSFX.Play();
            landGroundVFX.Play();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            landSFX.Play();
            landWallVFX.Play();
        }
    }


    //This code is copied from old Future present switcher.cs
    private void timeSwitch()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }

        //Teleporting player and camera method
        //Old control scheme, left click for present right click for future
        if (!oneClickOption)
        {
            //Teleport to present (left click)
            if (prsnt.WasPressedThisFrame() && (inFuture) && (timer <= 0f))
            {
                timeSwapVFX.SetActive(true);
                timeSwapSFX.Play();
                StartCoroutine(resetTimeSwapVFX());
                inFuture = !inFuture;
                transform.position = new Vector3(transform.position.x, transform.position.y - 300, transform.position.z);
                playerCam.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y - 300, playerCam.transform.position.z);
                timer = switchCooldown;
            }
            //Teleport to future (right click)
            if (futr.WasPressedThisFrame() && (!inFuture) && (timer <= 0f))
            {
                timeSwapVFX.SetActive(true);
                timeSwapSFX.Play();
                StartCoroutine(resetTimeSwapVFX());
                inFuture = !inFuture;
                transform.position = new Vector3(transform.position.x, transform.position.y + 300, transform.position.z);
                playerCam.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y + 300, playerCam.transform.position.z);
                timer = switchCooldown;
            }
        }
        //New control scheme, left click switches to the other time period
        else
        {
            //Teleport to present 
            if (prsnt.WasPressedThisFrame() && (inFuture) && (timer <= 0f))
            {
                inFuture = !inFuture;
                transform.position = new Vector3(transform.position.x, transform.position.y - 300, transform.position.z);
                playerCam.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y - 300, playerCam.transform.position.z);
                timer = switchCooldown;
                animatorPresent.SetBool("Change In Time Present", true);
                animatorPresentTwo.SetBool("Change In Time Present", true);
            }
            //Teleport to future
            if (prsnt.WasPressedThisFrame() && (!inFuture) && (timer <= 0f))
            {
                inFuture = !inFuture;
                transform.position = new Vector3(transform.position.x, transform.position.y + 300, transform.position.z);
                playerCam.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y + 300, playerCam.transform.position.z);
                timer = switchCooldown;
                animatorFuture.SetBool("Change In Time Future", true);
                animatorFutureTwo.SetBool("Change In Time Future", true);
            }

            //animate to future
            if (animatorPresent.GetCurrentAnimatorStateInfo(0).IsName("Change Time to Future"))
            {
                //Debug.Log("Change");
                presentCogs.SetActive(false);
                futureCogs.SetActive(true);
            }
            //animate to present 
            if (animatorFuture.GetCurrentAnimatorStateInfo(0).IsName("Change Time To Present"))
            {
                presentCogs.SetActive(true);
                futureCogs.SetActive(false);
            }
        }
    }
    
    private IEnumerator resetTimeSwapVFX()
    {
        yield return new WaitForSeconds(0.8f);
        timeSwapVFX.SetActive(false);
    }

}
