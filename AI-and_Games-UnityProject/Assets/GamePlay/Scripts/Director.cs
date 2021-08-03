using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Director : MonoBehaviour
{
	public static Director Instance
	{
		get; set;
	}

	[Header("Prefabs")]
	[SerializeField] GameObject playerGO;
	[SerializeField] GameObject prisonerGO;
	[SerializeField] GameObject guardGO;

	[Header("Audio")]
	[SerializeField] GameObject audioManagerPrefab;
	[SerializeField] public AudioDataBase audioDataBase;
	[Header("Game Config")]
	[SerializeField] float prequisitionInterval;
	[SerializeField] float prequisitionDuration;
	[Header("Game Dialogues")]
	[SerializeField] public List<string> newGameDialogue;
	[SerializeField] public List<string> getCaughtDialogue;
	[SerializeField] public List<string> successfulEscapedDialogue;
	[SerializeField] public List<string> playerUnlockToiletDialogue;
	[SerializeField] public List<string> playerUnlockPipeDialogue;

	public float prequisitionCounter;
	public bool isPrequisitioning;
	public bool IsInteractingWithUI
	{
		get; set;
	}
	int noExitUnlocked;

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
		StartGame();
	}

	private void StartGame()
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
		noExitUnlocked = 0;
		FindPlayerAndResetPosition();
		SetupPrisoners();


		void SetupPrisoners()
		{
			Prisoner[] prisoners = GameObject.FindGameObjectsWithTag("Prisoner").ToList().ConvertAll(e => e.GetComponent<Prisoner>()).ToArray();

			List<Inventorys.Inventory> list = PrisonerInventorys.GetPrisonerInventory();
			for (int i = 0; i < list.Count; i++)
			{
				if (i < prisoners.Length)
				{
					Inventorys.Inventory inventory = list[i];
					prisoners[i].AssignInventory(inventory);
					prisoners[i].GetInventory().owner = prisoners[i].gameObject;
				}
			}
		}
	}
	void FindPlayerAndResetPosition()
	{
		GameObject spawnPointGO = GameObject.Find("PlayerSpawnPoint");
		Vector3 playerSpawnPoint;
		if (spawnPointGO)
		{
			playerSpawnPoint = spawnPointGO.transform.position;
		}
		else
		{
			Debug.LogError("Couldn't find player spawn point in the scene");
			playerSpawnPoint = Vector3.zero;
		}

		GameObject playerGO = GameObject.FindWithTag("Player");
		if (!playerGO)
		{
			playerGO = Instantiate(playerGO);
		}
		playerGO.transform.position = playerSpawnPoint;
	}

	private void Update()
	{
		UpdateCounter();
	}

	private void UpdateCounter()
	{
		if (!IsInteractingWithUI)
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
		FindPlayerAndResetPosition();
		StartGetCaughtDialogue();
	}

	public void PlayerExit(Exit exit)
	{
		noExitUnlocked++;
		if (noExitUnlocked == 2)
		{
			PlayerSuccessTheGame();
		}
		else if (exit.name.Equals("ToiletExit"))
		{
			UIManager.Instance.SwitchToHudAndShowDialogue(null, null, playerUnlockToiletDialogue);
		}
		else
		{
			UIManager.Instance.SwitchToHudAndShowDialogue(null, null, playerUnlockPipeDialogue);
		}
	}

	public void GameEnd()
	{
		GameEvents.OnPrequisitionStart.RemoveAllListeners();
		GameEvents.OnPrequisitionEnd.RemoveAllListeners();
	}

	private void PlayerSuccessTheGame()
	{
		UIManager.Instance.SwitchToHudAndShowDialogue(null, null, successfulEscapedDialogue);
		GameEnd();
	}

	public void StartNewGameDialogue()
	{
		UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
		UIManager.Instance.GetScreenComponent<HudScreen>().InitializeAndShowDialoguePanel(null, null, newGameDialogue);
	}

	public void StartGetCaughtDialogue()
	{
		UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
		UIManager.Instance.GetScreenComponent<HudScreen>().InitializeAndShowDialoguePanel(null, null, getCaughtDialogue);
	}
}
