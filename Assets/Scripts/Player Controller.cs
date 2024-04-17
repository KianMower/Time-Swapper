using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//Movement using old input system
public class NewBehaviourScript : MonoBehaviour
{
    private float horizontalInput;
    private float speed = 8f;
    private float jumpingSpeed = 16f;
    private bool isFacingRight = true;

    //Double jump
    private bool doubleJump;


    //Dashing
    private bool dashAllowed = true;
    private bool isDashing;
    private float dashSpeed = 24f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1.5f;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;

    void Update()
    {
        //Prevents player inputting extra actions while dashing
        if(isDashing)
        {
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        //Jumping
        if(isGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if(Input.GetButtonDown("Jump") && (isGrounded() || doubleJump))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingSpeed);

            doubleJump = !doubleJump;
        }
        
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0.0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        //Trigger dash
        //On left shift for now, will change to double tap later :)
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashAllowed)
        {
            StartCoroutine(dash());
        }
        flipPlayer();
        
    }

    private void FixedUpdate()
    {
        //Prevents player inputting extra actions while dashing
        if (isDashing)
        {
            return;
        }

        //Takes horizontal input from update and sets speed 
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        //Used a overlap circle on the groundCheck game object to see if we are grounded
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
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
        }
    }

    private IEnumerator dash()
    {
        dashAllowed = false;
        isDashing = true;
        float startingGravity = rb.gravityScale;
        rb.gravityScale = 0; //Sets gravity to 0 so the player doesnt fall during the dash
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f ); //transform.localScale.x is player direction
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = startingGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        dashAllowed = true;
    }
}
