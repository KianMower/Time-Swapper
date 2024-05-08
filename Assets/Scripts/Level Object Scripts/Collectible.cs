using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //For this script to work, the collectible has to have a collider
    //With is trigger enabled

    enum collectibleType {
        Microchip,
        HealthPack,
    }

    [SerializeField] collectibleType type = new collectibleType();
    [SerializeField] private HealthController playerHealth;
    public float healthPackHealAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(type == collectibleType.Microchip)
            {
                playerHealth.microchipsCollected += 1;
                Destroy(gameObject);
            }
           // if (type == collectibleType.HealthPack)
           // {
                //Add health to player without exceeding max health
                //If the difference between max health and health >= 0.01, heal player and destroy health pack
                //if (playerHealth.maxHealth - playerHealth.health >= healthPackHealAmount)
               // {
                   // playerHealth.health += healthPackHealAmount;
                    //Destroy(gameObject); //Destroys health pack
                //}
               // else if (playerHealth.maxHealth - playerHealth.health >= 0.01f)
               // {
                 //   playerHealth.health = playerHealth.maxHealth;
                  //  Destroy(gameObject); //Destroys health pack
               // }
                //The player has max health, so ignore the health pack
                else
                {
                    return;
           //     }
            }
        }
    }
}
