using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class TemporarySceneNavigator : MonoBehaviour
{

    //O = tutorial, 1 = lv1, 2=lv2
    public float sceneToLoad;

    public void BeginTutorial()
    {
        SceneManager.LoadScene("Tutorial Scene"); // Loads tutorial on button click
    }

    public void BeginLevel1()
    {
        SceneManager.LoadScene("Level 1 Scene"); // Loads level 1 on button click
    }

    public void BeginLevel2()
    {
        SceneManager.LoadScene("Level 2 Scene"); // Loads level 2 on button click
    }

    public void BeginLevel3()
    {
        SceneManager.LoadScene("Level 3 Scene"); // Loads level 3 on button click
    }

    private void OnTriggerEnter2D(Collider2D collision)  //Section implemented by the team's programmer
    {
        if(collision.tag == "Player")
        {
            if(sceneToLoad == 0)
            {
                SceneManager.LoadScene("Tutorial Scene");
            }
            if (sceneToLoad == 1)
            {
                SceneManager.LoadScene("Level 1 Scene");
            }
            if (sceneToLoad == 2)
            {
                SceneManager.LoadScene("Level 2 Scene");
            }
            if (sceneToLoad == 3)
            {
                SceneManager.LoadScene("Level 3 Scene");
            }


        }
    }
}
