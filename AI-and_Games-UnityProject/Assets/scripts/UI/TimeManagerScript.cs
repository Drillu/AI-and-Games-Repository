using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagerScript : MonoBehaviour
{
    public float slowdownFactor = 0.1f;
    public float slowdownLenght = 2f;
    public bool isStopped = false;

    void Update()
    {
        if (isStopped == false) 
        { 
            Time.timeScale += (1 / slowdownLenght) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }

    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void DoStopTime(bool stop)
    {
        if (stop == true)
        {
            Time.timeScale = 0f;
            isStopped = stop;
        }
        else
        {
            Time.timeScale = 1f;
            isStopped = stop;
        }
    }
}
