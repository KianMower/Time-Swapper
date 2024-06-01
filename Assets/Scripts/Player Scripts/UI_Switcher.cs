using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Switcher : MonoBehaviour
{
    PlayerController player;
    [SerializeField] GameObject presentCogs;
    [SerializeField] GameObject futureCogs;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Disables and enables UI gameobjects dependent on time zone
        if (player.inFuture == true)
        {
            futureCogs.SetActive(true);
            presentCogs.SetActive(false);
        }
        else
        {
            futureCogs.SetActive(false);
            presentCogs.SetActive(true);
        }
    }
}
