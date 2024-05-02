using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHintEffect : MonoBehaviour
{
    [SerializeField] private Animator glowAnim;

    // Start is called before the first frame update
    void Start()
    {
        glowAnim.GetComponent<Animator>();

        glowAnim.Play("PlatformBlank");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            glowAnim.Play("PlatformHint");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            glowAnim.Play("PlatformBlank");
        }
    }
}
