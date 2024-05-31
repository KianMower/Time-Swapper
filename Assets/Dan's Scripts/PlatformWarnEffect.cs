using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformWarnEffect : MonoBehaviour
{
    [SerializeField] private Animator warnAnim;

    void Start()
    {
        warnAnim.GetComponent<Animator>(); // Sets the animator variable 

        warnAnim.Play("PlatformWarnBlank"); // Plays the starting animation where there is no effect
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            warnAnim.Play("PlatformWarningAnim"); // Activates the flashing animation when the player enters the collider
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            warnAnim.Play("PlatformWarnBlank"); // Deactivates the flashing animation when the player leaves the collider
        }
    }
}
