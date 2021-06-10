using System;
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
	private ScreenBase[] screens;
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
			InitializeScreens();
		}
	}

	private void Start() {
		foreach (ScreenBase screen in screens)
		{
			screen.Initialize();
		}
	}
	private void InitializeScreens()
	{
		screens = GetComponentsInChildren<ScreenBase>();
	}

	public void SwitchToScreen(ScreenType screenType, bool hideOthers = true)
	{
		foreach (ScreenBase screen in screens)
		{
			if (screen.screenType == screenType)
			{
				screen.gameObject.SetActive(true);
			}
			else
			{
				if (hideOthers)
				{
					screen.gameObject.SetActive(false);
				}
			}
		}
	}

	public T GetScreenComponent<T>() where T : ScreenBase
	{
		foreach (ScreenBase screen in screens)
		{
			if (screen.GetType() == typeof(T))
			{
				return screen.GetComponent<T>();
			}
		}
		return null;
	}

	public void HideAllScreens()
	{
		foreach (ScreenBase screen in screens)
		{
			screen.gameObject.SetActive(false);
		}
	}
}
