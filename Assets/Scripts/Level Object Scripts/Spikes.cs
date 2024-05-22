using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private bool spikesActive = true;
    public float gracePeriod;
    public float damageAmount;
    private HealthController healthController;

    //If player collides with a spike and its active, run spikesTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && spikesActive)
        {
            //Debug.Log("spikes hit player");
            healthController = collision.gameObject.GetComponent<HealthController>();
            StartCoroutine(spikesTrigger());
        }
    }

    //If player gets off spike, reactivate spike
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(spikesTrigger());
            spikesActive = true;
        }
    }

    //Damage player, wait grace period then reactivate spikes
    private IEnumerator spikesTrigger()
    {
        healthController.health -= damageAmount;
        spikesActive = false;
        yield return new WaitForSeconds(gracePeriod);
        spikesActive = true;
    }
}
