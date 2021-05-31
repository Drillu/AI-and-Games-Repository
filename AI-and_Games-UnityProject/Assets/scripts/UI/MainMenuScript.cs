using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Animator transition;
    public float transTime;

    IEnumerator LoadLevel(int indexLevel)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transTime);
        SceneManager.LoadScene(indexLevel);
    }

    public void PlayGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }
}
