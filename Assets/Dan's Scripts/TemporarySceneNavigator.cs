using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TemporarySceneNavigator : MonoBehaviour
{
    public void BeginTutorial()
    {
        SceneManager.LoadScene("Tutorial Scene");
    }

    public void BeginLevel1()
    {
        SceneManager.LoadScene("Level 1 Scene");
    }

    public void BeginLevel2()
    {
        SceneManager.LoadScene("Level 2 Scene");
    }

    public void BeginLevel3()
    {
        SceneManager.LoadScene("Level 3 Scene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene("TemporaryMenu");
        }
    }
}
