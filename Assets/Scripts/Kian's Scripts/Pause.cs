using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Pause : MonoBehaviour
{
    public FuturePresentSwitcher changeTime;

    public GameObject pauseMenu;

    public Animator pauseSlideIn;

    public bool gameIsPaused;

    public Camera gameCam;       

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            gameIsPaused = !gameIsPaused;           
        }       

        if (pauseSlideIn.GetCurrentAnimatorStateInfo(0).IsName("Pause Set"))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {        
        if (gameIsPaused)
        {
            Time.timeScale = 0f;            
            changeTime.enabled = false;
            gameCam.GetComponent<PostProcessLayer>().enabled = true;
        }
        else
        {
            Time.timeScale = 1;            
            pauseMenu.SetActive(false);
            changeTime.enabled = true;
            gameCam.GetComponent<PostProcessLayer>().enabled = false;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void replay()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
