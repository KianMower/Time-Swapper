using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class livesScript : MonoBehaviour
{
    private HealthController healthController;
    public int lives = 3;
    // Start is called before the first frame update
    void Start()
    {
        healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lives <= 0)
        {
            Debug.Log("game over");
            SceneManager.LoadScene("No Lives");
        }
    }
}
