using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AI;

public class Director : MonoBehaviour
{
	public static Director Instance
	{
		get; set;
	}

	[Header("Scenes")]
	[SerializeField] AssetReference gameSceneRef;
	[SerializeField] AssetReference mainMenuSceneRef;
	AsyncOperationHandle<SceneInstance> gameSceneHandler;
	AsyncOperationHandle<SceneInstance> mainMenuHandler;

	[Header("Prefabs")]
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
	[SerializeField] public List<string> getCaughtGuardDialogue;
	[SerializeField] public List<string> successfulEscapedDialogue;
	[SerializeField] public List<string> playerUnlockToiletDialogue;
	[SerializeField] public List<string> playerUnlockPipeDialogue;
	[SerializeField] public List<string> playerUnlockPipeAndToiletDialogue;

	private bool isGaming;
	public float prequisitionCounter;
	public bool isPrequisitioning;
	public bool IsInteractingWithUI
	{
		get; set;
	}
	bool isTalkingToGuard;

	bool toiletUnlocked = false;
	bool pipeUnlocked = false;

	private void Awake()
	{
		if(Instance && Instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}

		LoadAudioManager();
		StartBGM();
	}
	private void Update()
	{
		if(isGaming)
		{
			UpdateCounter();
		}
	}

	public void LoadGameScene()
	{
		gameSceneHandler = Addressables.LoadSceneAsync(gameSceneRef);
		gameSceneHandler.Completed += (handler) =>
		{
			if(gameSceneHandler.Status == AsyncOperationStatus.Succeeded)
			{
				StartGame();
			}
			else
			{
				Debug.LogError("Addressables: Load Game Scene failed" + gameSceneHandler.OperationException.ToString());
			}
		};
	}

	private void StartGame()
	{
		// ui
		UIManager.Instance.Initialize();
		UIManager.Instance.HideAllScreens();
		UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
		// requisition
		GameEvents.OnPrequisitionStart.AddListener(StartPrequisition);
		GameEvents.OnPrequisitionEnd.AddListener(EndPrequisition);
		prequisitionCounter = prequisitionInterval;

		toiletUnlocked = false;
		pipeUnlocked = false;
		FindPlayerAndResetPosition();
		SetupPrisoners();
		isGaming = true;

		Agent player = GameObject.FindGameObjectWithTag("Player").GetComponent<Agent>();
		UIManager.Instance.SwitchToHudAndShowDialogue(player.GetIconSprite(), player.GetAgentName(), newGameDialogue);

		void SetupPrisoners()
		{
			Prisoner[] prisoners = GameObject.FindGameObjectsWithTag("Prisoner").ToList().ConvertAll(e => e.GetComponent<Prisoner>()).ToArray();

			List<Inventorys.Inventory> list = PrisonerInventorys.GetPrisonerInventory();
			for(int i = 0; i < list.Count; i++)
			{
				if(i < prisoners.Length)
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
		if(spawnPointGO)
		{
			playerSpawnPoint = spawnPointGO.transform.position;
		}
		else
		{
			Debug.LogError("Couldn't find player spawn point in the scene");
			playerSpawnPoint = Vector3.zero;
		}

		GameObject playerGO = GameObject.FindWithTag("Player");
		if(!playerGO)
		{
			playerGO = Instantiate(playerGO);
		}
		playerGO.GetComponent<NavagentMover>().StopMoving();
		playerGO.GetComponent<NavagentMover>().ClearDestination();
		playerGO.GetComponent<NavagentMover>().Warp(playerSpawnPoint);
	}

	public void PlayerSuccessTheGame()
	{
		UIManager.Instance.SwitchToHudAndShowDialogue(null, null, successfulEscapedDialogue, dialogueFinished: () =>
		 {
			 StartCoroutine(GameEnd());
		 });
	}

	public IEnumerator GameEnd()
	{
		isGaming = false;
		GameEvents.OnPrequisitionStart.RemoveAllListeners();
		GameEvents.OnPrequisitionEnd.RemoveAllListeners();

		UIManager.Instance.SwitchToScreen(UIManager.ScreenType.TransitionScreen);
		UIManager.Instance.GetScreenComponent<TransitionScreen>().SetTransitionText("Thanks for playing!");
		UIManager.Instance.GetScreenComponent<TransitionScreen>().FadeOut(2f);
		yield return new WaitForSeconds(3f);
		mainMenuHandler = Addressables.LoadSceneAsync(mainMenuSceneRef, LoadSceneMode.Single);
		mainMenuHandler.Completed += MainMenuHandler_Completed;
	}

	private void MainMenuHandler_Completed(AsyncOperationHandle<SceneInstance> obj)
	{
		UIManager.Instance.GetScreenComponent<TransitionScreen>().SetTransitionText(string.Empty);
		UIManager.Instance.GetScreenComponent<TransitionScreen>().FadeIn(2f);
	}

	public void QuitApp()
	{
		Application.Quit();
	}


	private void UpdateCounter()
	{
		if(!IsInteractingWithUI)
		{
			prequisitionCounter -= Time.deltaTime;
			if(prequisitionCounter <= 0)
			{
				isPrequisitioning = !isPrequisitioning;
				if(isPrequisitioning)
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
		UIManager.Instance.GetScreenComponent<HudScreen>().InitializeAndShowDialoguePanel(p.GetIconSprite(), p.GetAgentName(), p.Speach, isTrading: true);
		UIManager.Instance.GetScreenComponent<HudScreen>().currentInteractingInventory = p.GetInventory();
	}

	public Inventorys.Inventory GetPlayerInventory()
	{
		GameObject playerGO = GameObject.FindWithTag("Player");
		Player player = playerGO.GetComponent<Player>();
		return player.GetInventory();
	}

	public void StartCountDown()
	{

	}

	private void LoadAudioManager()
	{
		if(!AudioManager.Instance)
		{
			Instantiate(audioManagerPrefab);
		}
	}

	private void StartBGM()
	{
		AudioManager.Instance.PlayMusicOnLayer(AudioManager.MusicLayer.Primary, audioDataBase.suspension);
		AudioManager.Instance.PlayMusicOnLayer(AudioManager.MusicLayer.Secondary, audioDataBase.action);
		AudioManager.Instance.SetMusicLayerTrackVolumn(AudioManager.MusicLayer.Secondary, 0);
	}

	public void GuardCaughtPlayer(Guard guard, Player player)
	{
		if(!isTalkingToGuard)
		{
			isTalkingToGuard = true;
			player.GetComponent<NavagentMover>().StopMoving();
			player.GetComponent<NavagentMover>().ClearDestination();
			UIManager.Instance.SwitchToHudAndShowDialogue(guard.GetIconSprite(), guard.GetAgentName(), getCaughtGuardDialogue, dialogueFinished: () =>
			{
				StartCoroutine(GuardCaughtPlayerCR(player));
			});
		}
	}

	IEnumerator GuardCaughtPlayerCR(Player player)
	{
		UIManager.Instance.SwitchToScreen(UIManager.ScreenType.TransitionScreen);
		UIManager.Instance.GetScreenComponent<TransitionScreen>().SetTransitionText(string.Empty);
		UIManager.Instance.GetScreenComponent<TransitionScreen>().FadeOut(2);
		yield return new WaitForSeconds(3);
		FindPlayerAndResetPosition();
		UIManager.Instance.GetScreenComponent<TransitionScreen>().FadeIn(2f);
		yield return new WaitForSeconds(3);
		StartGetCaughtDialogue();
		isTalkingToGuard = false;
	}

	public void PlayerExit(Exit exit)
	{
		GameObject playerGO = GameObject.FindWithTag("Player");
		Player player = playerGO.GetComponent<Player>();
		if(exit.exitType == Exit.ExitType.Toilet)
		{
			toiletUnlocked = true;
			if(pipeUnlocked)
			{
				PlayerSuccessTheGame();
			}
			else
			{
				UIManager.Instance.SwitchToHudAndShowDialogue(player.GetIconSprite(), player.GetAgentName(), playerUnlockToiletDialogue);
			}
		}
		else if(exit.exitType == Exit.ExitType.Pipe)
		{
			pipeUnlocked = true;
			if(toiletUnlocked)
			{
				UIManager.Instance.SwitchToHudAndShowDialogue(player.GetIconSprite(), player.GetAgentName(), playerUnlockPipeAndToiletDialogue);
			}
			else
			{
				UIManager.Instance.SwitchToHudAndShowDialogue(player.GetIconSprite(), player.GetAgentName(), playerUnlockPipeDialogue);
			}

		}
	}

	public void StartNewGameDialogue()
	{
		GameObject playerGO = GameObject.FindWithTag("Player");
		Player player = playerGO.GetComponent<Player>();
		UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
		UIManager.Instance.GetScreenComponent<HudScreen>().InitializeAndShowDialoguePanel(player.GetIconSprite(), player.GetAgentName(), newGameDialogue);
	}

	public void StartGetCaughtDialogue()
	{
		GameObject playerGO = GameObject.FindWithTag("Player");
		Player player = playerGO.GetComponent<Player>();
		UIManager.Instance.SwitchToHudAndShowDialogue(player.GetIconSprite(), player.GetAgentName(), getCaughtDialogue);
	}

}
