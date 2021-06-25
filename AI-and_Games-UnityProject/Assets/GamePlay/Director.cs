using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
	public static Director Instance { get; set; }
	[Header("Audio")]
	[SerializeField] GameObject audioManagerPrefab;
	[SerializeField] public AudioDataBase audioDataBase;
	public bool IsInteractingWithUI { get; set; }

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
		LoadManagers();
		AudioManager.Instance.PlayMusicOnLayer(AudioManager.MusicLayer.Primary, audioDataBase.suspension);
		AudioManager.Instance.PlayMusicOnLayer(AudioManager.MusicLayer.Secondary, audioDataBase.action);
		AudioManager.Instance.SetMusicLayerTrackVolumn(AudioManager.MusicLayer.Secondary, 0);
		// UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
		// UIManager.Instance.GetScreenComponent<HudScreen>().InitializeAndShowPlayerInventoryPanel();
		UIManager.Instance.Initialize();
		UIManager.Instance.HideAllScreens();
		UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
	}

	public void TalkToPrisoner(Prisoner p)
	{
		UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
		UIManager.Instance.GetScreenComponent<HudScreen>().InitializeAndShowDialoguePanel(null, p.name, p.Speach);
		UIManager.Instance.GetScreenComponent<HudScreen>().currentInteractingObject = p;
	}

	public Inventorys.Inventory GetPlayerInventory()
	{
		return Player.Instance.GetInventory();
	}

	private void LoadManagers()
	{
		if (!AudioManager.Instance)
		{
			Instantiate(audioManagerPrefab);
		}
	}
}
