using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPadSpeed;

    public AudioSource jumpPadSFX;
    public ParticleSystem jumpPadVFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If player enters jump pad trigger area
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("found.");
            jumpPadSFX.Play();
            jumpPadVFX.Play();
            //Get players rigid body component and add force
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPadSpeed, ForceMode2D.Impulse);
        }

    }
}
