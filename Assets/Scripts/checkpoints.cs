using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoints : MonoBehaviour
{
    public HealthController player;
    public string timezone;
    private bool activated = false;

    //Disable sprite renderer for children gameobjects (makes it visible in editor only)
    private void Start()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        { 
            foreach(var sr in renderers)
            {
                sr.enabled = false;
            }
        }
    }
    //If checkpoint triggered and not activated, activate it and update respawn pos and timezone.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!activated)
        {
            activated = true;
            player.respawnPos = transform.position;
            player.respawnTimeZone = timezone;
            Debug.Log(player.respawnTimeZone);
        }
    }
}
