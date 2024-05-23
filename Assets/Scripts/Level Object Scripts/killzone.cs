using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//If player tagged game object enters trigger area, set health to 0
public class killzone : MonoBehaviour
{
    [SerializeField] private HealthController playerHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
        }
    }
}
