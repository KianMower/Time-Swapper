using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TemporarySceneNavigator : MonoBehaviour
{
    public void BeginTutorial()
    {
        SceneManager.LoadScene("Dan'sScene");
    }

    public void BeginLevel1()
    {
        SceneManager.LoadScene("Dan's Scene Level 1");
    }

    public void BeginLevel2()
    {
        SceneManager.LoadScene("Dan's Scene Level 2");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene("TemporaryMenu");
        }
    }
}
