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

	public void StartGame()
	{
		Director.Instance.LoadGameScene();
	}

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }

    public void ToMenu()
    {
        StartCoroutine(LoadLevel(0));
        Time.timeScale = 1f;
    }

	public void ManualEnd()
	{
		Director.Instance.PlayerSuccessTheGame();
	}

	public void Quit()
	{
		Director.Instance.QuitApp();
	}
}
