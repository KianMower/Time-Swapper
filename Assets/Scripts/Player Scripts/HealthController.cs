using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float maxHealth;
    public float health;
    private float healthBarSize;
    public float microchipsCollected;
    [SerializeField] Scrollbar healthBar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) 
        {
            health -= 1;
            Debug.Log("Collided with enemy");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
        }
    }
}
