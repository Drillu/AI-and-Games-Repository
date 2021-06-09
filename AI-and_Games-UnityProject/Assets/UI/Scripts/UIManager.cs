using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public enum ScreenType
	{
		IntroScreen,
		HudScreen,
		MenuSelectionScreen,
		SettingScreen,
		TransitionScreen,
	}

	public static UIManager Instance { get; set; }
	private void Awake()
	{
		if (Instance && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
	}


}
