using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TemporaryDeath : MonoBehaviour
{
    [SerializeField] private HealthController playerHealth; // Creates a link to the programmer's player health script
    public ParticleSystem electricity; // The damage VFX
    public AudioSource damageSFX; // The damage SFX

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.healthCounter <= 0) // Calls the death function when the health is 0 or less than 0
        {
            death(); 
        }
    }

    public void death()
    {
        Debug.Log("death ran");
        SceneManager.LoadScene("TemporaryMenu"); // Resets to the level select when death happens
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy") //On collision with an enemy this plays the VFX and SFX for damaging while subtracting health
        {
            electricity.Play();
            damageSFX.Play();
            playerHealth.healthCounter -= 25;
        }
    }

    private IEnumerator damageVFXReset()
    {
        yield return new WaitForSeconds(2);
    }
}
