using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Director : MonoBehaviour
{
	public static Director Instance { get; set; }
	[Header("Audio")]
	[SerializeField] GameObject audioManagerPrefab;
	[SerializeField] public AudioDataBase audioDataBase;
	[Header("Game Config")]
	[SerializeField] float prequisitionInterval;
	[SerializeField] float prequisitionDuration;
	public float prequisitionCounter;
	public bool isPrequisitioning;
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
		// audio
		AudioManager.Instance.PlayMusicOnLayer(AudioManager.MusicLayer.Primary, audioDataBase.suspension);
		AudioManager.Instance.PlayMusicOnLayer(AudioManager.MusicLayer.Secondary, audioDataBase.action);
		AudioManager.Instance.SetMusicLayerTrackVolumn(AudioManager.MusicLayer.Secondary, 0);
		// ui
		UIManager.Instance.Initialize();
		UIManager.Instance.HideAllScreens();
		UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
		// requisition
		GameEvents.OnPrequisitionStart.AddListener(StartPrequisition);
		GameEvents.OnPrequisitionEnd.AddListener(EndPrequisition);
		prequisitionCounter = prequisitionInterval;
	}

	private void Update()
	{
		UpdateCounter();
	}

	private void UpdateCounter()
	{
		prequisitionCounter -= Time.deltaTime;
		if (prequisitionCounter <= 0)
		{
			isPrequisitioning = !isPrequisitioning;
			if (isPrequisitioning)
			{
				prequisitionCounter = prequisitionDuration;
				GameEvents.OnPrequisitionStart?.Invoke();
			}
			else
			{
				prequisitionCounter = prequisitionInterval;
				GameEvents.OnPrequisitionEnd?.Invoke();
			}
		}
		UIManager.Instance.GetScreenComponent<HudScreen>().UpdateCountdownText(prequisitionCounter);
	}

	private void StartPrequisition()
	{
		AudioManager.Instance.SetMusicLayerTrackVolumn(AudioManager.MusicLayer.Secondary, 1);
		AudioManager.Instance.FadeinMusicOnLayer(AudioManager.MusicLayer.Secondary);
	}

	private void EndPrequisition()
	{
		AudioManager.Instance.FadeoutMusicOnLayer(AudioManager.MusicLayer.Secondary);
	}

	public void TalkToPrisoner(Prisoner p)
	{
		UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
		UIManager.Instance.GetScreenComponent<HudScreen>().InitializeAndShowDialoguePanel(null, p.name, p.Speach, isTrading: true);
		UIManager.Instance.GetScreenComponent<HudScreen>().currentInteractingInventory = p.GetInventory();
	}

	public Inventorys.Inventory GetPlayerInventory()
	{
		return Player.Instance.GetInventory();
	}

	public void StartCountDown()
	{

	}

	private void LoadManagers()
	{
		if (!AudioManager.Instance)
		{
			Instantiate(audioManagerPrefab);
		}
	}
	public void GuardCaughtPlayer()
	{
		Debug.Log($"<color=red>Guard caught player during prequisition!</color>");
		Time.timeScale = 0;
	}
	public void PlayerExit(Exit exit)
	{

	}
}
