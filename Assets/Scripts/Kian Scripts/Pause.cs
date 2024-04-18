using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public ChangeTime changeTime;

    public GameObject pauseMenu;

    public static bool gameIsPaused;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            changeTime.enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            changeTime.enabled = true;
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
