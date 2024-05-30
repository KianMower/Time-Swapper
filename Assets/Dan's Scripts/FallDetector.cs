using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private Vector3 respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position; // Sets the player respawn point to where the player starts the scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallDetector")
        {
            transform.position = respawnPoint; // Resets the player's position to where the respawn point was set
        }
        else if(collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position; // Sets the respawn point to be where the player interacted with a checkpoint
        }
    }
}
