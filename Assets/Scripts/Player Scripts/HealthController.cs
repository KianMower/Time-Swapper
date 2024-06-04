using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{  
    public int microchipsCollected;
    public int healthCounter = 3;
    public int maxHealth = 3;
   
    //Checkpoint values
    public PlayerController timeSwitcher;
    public Vector3 respawnPos;
   
    public GameObject playerCam;
    [SerializeField] GameObject[] health;
    public string respawnTimeZone = "present";
    livesScript lives;

    public AudioSource damageSFX;
    public ParticleSystem damageVFX;

    private void Start()
    {
        lives = GetComponent<livesScript>();
        healthCounter = maxHealth;
        respawnPos = transform.position;   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)" || collision.CompareTag("Enemy"))
        {
            damageSFX.Play();
            damageVFX.Play();
            healthCounter -= 1;

            if (healthCounter == 2)
            {
                health[0].SetActive(false);
                health[3].SetActive(false);
                health[6].SetActive(false);
                health[9].SetActive(false);
            }

            if (healthCounter == 1)
            {
                health[1].SetActive(false);
                health[4].SetActive(false);
                health[7].SetActive(false);
                health[10].SetActive(false);
            }

            if (healthCounter == 0)
            {
                health[2].SetActive(false);
                health[5].SetActive(false);
                health[8].SetActive(false);
                health[11].SetActive(false);
            }
        }

        if(collision.tag == "Repair")
        {
            if (healthCounter == 2)
            {
                health[0].SetActive(true);
                health[3].SetActive(true);
                health[6].SetActive(true);
                health[9].SetActive(true);
            }

            if (healthCounter == 1)
            {
                health[1].SetActive(true);
                health[4].SetActive(true);
                health[7].SetActive(true);
                health[10].SetActive(true);
            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if(healthCounter == 3)
        {
            resetUI();
        }

        if(healthCounter == 0)
        {
            if (respawnTimeZone == "present")
            {
                timeSwitcher.inFuture = false;
                //playerCam.transform.position = respawnPos;
                transform.position = respawnPos;
                //Debug.Log(timeSwitcher.inFuture)
            }
            if (respawnTimeZone == "future")
            {
                timeSwitcher.inFuture = true;
                //playerCam.transform.position = respawnPos;
                transform.position = respawnPos;
                //Debug.Log(timeSwitcher.inFuture);
            }

            healthCounter = maxHealth;
            resetUI();
            lives.lives -= 1;
        }        
    }
    //resets health UI on death
    void resetUI()
    {
        health[0].SetActive(true);
        health[1].SetActive(true);
        health[2].SetActive(true);
        health[3].SetActive(true);
        health[4].SetActive(true);
        health[5].SetActive(true);
        health[6].SetActive(true);
        health[7].SetActive(true);
        health[8].SetActive(true);
        health[9].SetActive(true);
        health[10].SetActive(true);
        health[11].SetActive(true);
    }

}
