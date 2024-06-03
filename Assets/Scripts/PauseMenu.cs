using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GamePaused = false;
    public GameObject PauseCanvasUI;

    public PlayerController playerController;

    public GameObject OtherUI;

    private void Start()
    {
        Time.timeScale = 1f;
        GamePaused = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        PauseCanvasUI.SetActive(false);
        //OtherUI.SetActive(true);
        Time.timeScale = 1f;
        GamePaused = false;
        playerController.OnEnable();
    }
    public void Pause()
    {
        PauseCanvasUI.SetActive(true);
       //OtherUI.SetActive(false);
        Time.timeScale = 0f;
        GamePaused = true;
        playerController.OnDisable();
        
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Retry");
        Time.timeScale = 1f;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu FInalised");
        Debug.Log("Exit");


       
    }

}
