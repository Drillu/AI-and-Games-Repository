using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        isPaused = true;
    }

  

}
