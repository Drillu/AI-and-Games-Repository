using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
	public static Director Instance { get; set; }
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

	private void Start()
	{
		// UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
		// UIManager.Instance.GetScreenComponent<HudScreen>().InitializeAndShowPlayerInventoryPanel();
		UIManager.Instance.HideAllScreens();
	}

	public void TalkToPrisoner(Prisoner p)
	{
		UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
		UIManager.Instance.GetScreenComponent<HudScreen>().InitializeAndShowDialoguePanel(null, p.name, p.Speach);
	}

}