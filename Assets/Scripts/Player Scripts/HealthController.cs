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
    
   
    //If collide with bullet, take damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            Debug.Log("OW");
            health -= 25;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Inits health and health bar
        health = maxHealth;
        healthBarSize = health / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Update health bar size
        healthBarSize = health / maxHealth;
        healthBar.size = healthBarSize;
        if (health <= 0)
        {
            Debug.Log("Player died");
            //Resets player to present checkpoint
            if(respawnTimeZone == "present")
            {
                health = maxHealth;
                timeSwitcher.inFuture = false;
                playerCam.transform.position = respawnPos;
                transform.position = respawnPos;
                Debug.Log(timeSwitcher.inFuture);

            }
            //Resets player to future checkpoint
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
