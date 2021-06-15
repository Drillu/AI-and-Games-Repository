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
	Queue<ScreenBase> currentActiveScreens = new Queue<ScreenBase>();
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

	public void Initialize()
	{
		foreach (ScreenBase screen in screens)
		{
			screen.Initialize();
		}
	}
	private void InitializeScreens()
	{
		screens = GetComponentsInChildren<ScreenBase>(true);
	}

	public void SwitchToScreen(ScreenType screenType, bool hideOthers = true)
	{
		if (hideOthers)
		{
			currentActiveScreens.Clear();
		}
		foreach (ScreenBase screen in screens)
		{
			if (screen.screenType == screenType)
			{
				screen.gameObject.SetActive(true);
				currentActiveScreens.Enqueue(screen);
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

	private void Update()
	{
		// no screen active, we can listen to input and initialize screens correspondingly
		if (currentActiveScreens.Count <= 0)
		{
			if (InputManager.Instance.IsShowInventoryButtonPressed)
			{
				SwitchToScreen(ScreenType.HudScreen);
				GetScreenComponent<HudScreen>().InitializeAndShowPlayerInventoryPanel();
			}
		}
		else
		{
			ScreenBase currentActiveScreen = currentActiveScreens.Peek();
			if (!currentActiveScreen.ListenToInput())
			{
				currentActiveScreens.Dequeue();
			}
		}
	}
}
