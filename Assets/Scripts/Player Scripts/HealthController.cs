using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{  
    public float microchipsCollected;
    public int healthCounter = 3;
   
    //Checkpoint values
    public FuturePresentSwitcher timeSwitcher;
    public Vector3 respawnPos;
    public GameObject playerCam;
    [SerializeField] GameObject[] health;
    public string respawnTimeZone = "present";
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            Debug.Log("OW");
            healthCounter -= 1;

            if (healthCounter == 2)
            {
                health[0].SetActive(false);
                health[3].SetActive(false);
            }

            if (healthCounter == 1)
            {
                health[1].SetActive(false);
                health[4].SetActive(false);
            }

            if (healthCounter == 0)
            {
                health[2].SetActive(false);
                health[5].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {       
        if (respawnTimeZone == "present")
        {           
            timeSwitcher.inFuture = false;
            playerCam.transform.position = respawnPos;
            transform.position = respawnPos;
            Debug.Log(timeSwitcher.inFuture);

        }

        if (respawnTimeZone == "future")
        {
            timeSwitcher.inFuture = true;
            playerCam.transform.position = respawnPos;
            transform.position = respawnPos;
            Debug.Log(timeSwitcher.inFuture);
        }        
    }
}
