using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSwapVFX : MonoBehaviour
{
    public float TimerDuration = 30f;
    public Slider loadingWheel;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = TimerDuration;
    }

    // Update is called once per frame
    void Update()
    {
        VFXPlay();
    }

    private void VFXPlay()
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
