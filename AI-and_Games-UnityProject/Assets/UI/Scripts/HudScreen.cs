using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HudScreen : ScreenBase
{
	[Header("Gadgets")]
	[SerializeField] GameObject countDown;
	[SerializeField] TMPro.TextMeshProUGUI countDownText;

	[Header("Panels")]
	[SerializeField] List<HudScreenPanel> hudScreenPanels;
	public Inventorys.Inventory currentInteractingInventory;
	Stack<HudScreenPanel> currentActivePanels = new Stack<HudScreenPanel>();

	public override void Initialize()
	{
		hudScreenPanels.ForEach(panel => panel.Initialize(this));
		HideAllPanels();
	}

	private void Update()
	{
		if (IsCurrentActiveScreen)
		{
			ListenToInput();
		}
	}

	public override bool ListenToInput()
	{
		if (currentActivePanels.Count <= 0)
		{
			return false;
		}
		HudScreenPanel currentPanel = currentActivePanels.Peek();
		if (!currentPanel.ListenToInput())
		{
			currentActivePanels.Pop();
		}
		return currentActivePanels.Count > 0;
	}

	public void CurrentPanelCancelled(HudScreenPanel currentPanel)
	{
		if (currentPanel == currentActivePanels.Peek())
		{
			currentActivePanels.Pop();
			if (currentActivePanels.Count <= 0)
			{
				UIManager.Instance.QuitCurrentScreen(this);
			}
		}
	}


	public void InitializeAndShowDialoguePanel(Sprite icon, string charname, List<string> texts, bool hideOtherPanels = true, bool isTrading = false)
	{
		DialoguePanel panel = SwitchToPanel<DialoguePanel>();
		panel.InitializeAndStartDialogue(icon, charname, texts, isTrading: isTrading);
	}

	public void InitializeAndShowTradePanel(Inventorys.Inventory firstInventory, Inventorys.Inventory secondInventory, bool hideOtherPanels = true, TradePanel.TradeTarget target = TradePanel.TradeTarget.Prisoner)
	{
		TradePanel panel = SwitchToPanel<TradePanel>();
		panel.SetupTradePanel(firstInventory, secondInventory, target: target);
	}

	public void InitializeAndShowPlayerInventoryPanel(bool hideOtherPanels = false)
	{
		PlayerInventoryPanel panel = SwitchToPanel<PlayerInventoryPanel>();
		panel.Initialize();
	}

	public void InitializeAndShowCollectItemPanel(string formatter, string item)
	{
		CollectItemPanel panel = SwitchToPanel<CollectItemPanel>();
		panel.InitializeAndShow(string.Format(formatter, $"<color=green>{item}</color>"));
	}
	public void HideAllPanels()
	{
		hudScreenPanels.ForEach(panel => panel.gameObject.SetActive(false));
	}

	public void OnTradeButtonClicked()
	{
		InitializeAndShowTradePanel(currentInteractingInventory, Director.Instance.GetPlayerInventory());
	}

	public void OnByebyeButtonClicked()
	{
		currentActivePanels.Pop().gameObject.SetActive(false);
		currentActivePanels.Clear();
		UIManager.Instance.QuitCurrentScreen(this);
	}
	private T SwitchToPanel<T>(bool hideOthers = true) where T : HudScreenPanel
	{
		T result = null;
		foreach (HudScreenPanel panel in hudScreenPanels)
		{
			if (panel is T)
			{
				panel.gameObject.SetActive(true);
				if (hideOthers)
				{
					currentActivePanels.Clear();
				}
				currentActivePanels.Push(panel);
				result = panel as T;
			}
			else
			{
				if (hideOthers)
				{
					panel.gameObject.SetActive(false);
				}
			}
		}
		return result;
	}

	public void SetCountdownActive(bool active)
	{
		countDown.SetActive(active);
	}

	public void UpdateCountdownText(float countDown)
	{
		countDownText.color = Director.Instance.isPrequisitioning ? Color.red : Color.green;
		countDownText.text = string.Empty;
		string[] counter = countDown.ToString("F2").Split('.');
		countDownText.text += counter[0] + ".";
		countDownText.text += $"<size=70%>{counter[1]}</size>";
	}
}
