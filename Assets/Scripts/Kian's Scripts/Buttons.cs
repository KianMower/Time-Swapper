using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public Pause pause;
    public PlayerController futurePresentSwitcher;

    public GameObject mainMenu;
    public GameObject optionMenu;

    public Camera gameCam;

    public void Play()
    {
        Debug.Log("play clicked");
        SceneManager.LoadScene("Level 1 Scene");
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {

    }

    public void UnPause()
    {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
        optionMenu.SetActive(false);
        pause.gameIsPaused = false;
        futurePresentSwitcher.enabled = true;
        gameCam.GetComponent<PostProcessLayer>().enabled = false;
    }
}
