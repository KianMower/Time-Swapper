using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class killzone : MonoBehaviour
{
    [SerializeField] private HealthController playerHealth;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("test");
            playerHealth.health = 0;
        }
    }


}
