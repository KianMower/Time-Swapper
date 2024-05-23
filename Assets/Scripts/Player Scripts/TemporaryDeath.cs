using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TemporaryDeath : MonoBehaviour
{
    [SerializeField] private HealthController playerHealth;
    public ParticleSystem electricity;
    public AudioSource damageSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.health <= 0)
        {
            death();
        }
    }

    public void death()
    {
        Debug.Log("death ran");
        SceneManager.LoadScene("TemporaryMenu");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            electricity.Play();
            damageSFX.Play();
            playerHealth.health -= 25;
        }
    }

    private IEnumerator damageVFXReset()
    {
        yield return new WaitForSeconds(2);
    }
}
