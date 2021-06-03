using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    private float miniseconds,seconds,minutes;
    private TMP_Text timer;
  
    void Start()
    {
        timer = GetComponent<TMP_Text>();
        timer.text = " ";
    }
 
    void Update()
    {
        miniseconds = (int)(Time.time * 100) % 100;
        seconds = (int)(Time.time % 60);
        minutes = (int)(Time.time / 60);
        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + miniseconds.ToString("00");
    }
   
}
