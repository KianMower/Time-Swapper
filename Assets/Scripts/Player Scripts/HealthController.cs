using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public float maxHealth;
    public float health;
    private float healthBarSize;
    public float microchipsCollected;
    [SerializeField] Scrollbar healthBar;

    //Checkpoint values
    public FuturePresentSwitcher timeSwitcher;
    public Vector3 respawnPos;
    public GameObject playerCam;
    public string respawnTimeZone = "present";
    public ParticleSystem electricity;
    public AudioSource damageSFX;
    
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            electricity.Play();
            damageSFX.Play();
            Debug.Log("OW");
            health -= 25;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //damageVFX.SetActive(false);
        health = maxHealth;
        healthBarSize = health / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarSize = health / maxHealth;
        healthBar.size = healthBarSize;
        if (health <= 0)
        {
            Debug.Log("Player died");

            if(respawnTimeZone == "present")
            {
                health = maxHealth;
                timeSwitcher.inFuture = false;
                playerCam.transform.position = respawnPos;
                transform.position = respawnPos;
                Debug.Log(timeSwitcher.inFuture);

            }

            if(respawnTimeZone == "future")
            {
                health = maxHealth;
                timeSwitcher.inFuture = true;
                playerCam.transform.position = respawnPos;
                transform.position = respawnPos;
                Debug.Log(timeSwitcher.inFuture);
            }
        }
    }
}
