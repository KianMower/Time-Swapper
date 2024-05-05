using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformWarnEffect : MonoBehaviour
{
    [SerializeField] private Animator warnAnim;

    void Start()
    {
        warnAnim.GetComponent<Animator>();

        warnAnim.Play("PlatformWarnBlank");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            warnAnim.Play("PlatformWarningAnim");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            warnAnim.Play("PlatformWarnBlank");
        }
    }
}
