using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;

    public void Play()
    {

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
}
