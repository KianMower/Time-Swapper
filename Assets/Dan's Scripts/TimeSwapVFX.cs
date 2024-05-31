using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSwapVFX : MonoBehaviour
{
    public float TimerDuration = 30f; // Sets the timer duration
    public Slider loadingWheel; // The slider in the canvas
    float timer; // Float to be used to help trigger the effect

    // Start is called before the first frame update
    void Start()
    {
        timer = TimerDuration; // Sets the timer variable to the same time as the duration
    }

    // Update is called once per frame
    void Update()
    {
        VFXPlay();
    }

    private void VFXPlay() // The void where the VFX is supposed to be triggered
    {
        timer -= Time.deltaTime;

        float normalizedTime = Mathf.Clamp01(timer / TimerDuration);
        loadingWheel.value = normalizedTime;

        if (timer <= 0)
        {
            timer = 0;
        }
    }
}
