using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHintEffect : MonoBehaviour
{
    [SerializeField] private Animator glowAnim; // The animator for the effect

    // Start is called before the first frame update
    void Start()
    {
        glowAnim.GetComponent<Animator>(); // Sets the animator variable 

        glowAnim.Play("PlatformBlank"); // Plays the starting animation where there is no effect
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            glowAnim.Play("PlatformHint"); // Activates the flashing animation when the player enters the collider
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            glowAnim.Play("PlatformBlank"); // Deactivates the flashing animation when the player leaves the collider
        }
    }
}
