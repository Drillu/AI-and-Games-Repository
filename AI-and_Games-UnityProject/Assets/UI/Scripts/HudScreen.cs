using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudScreen : ScreenBase
{

	[SerializeField] List<HudScreenPanel> hudScreenPanels;
	public IInteractable currentInteractingObject;
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


	public void InitializeAndShowDialoguePanel(Sprite icon, string charname, string text, bool hideOtherPanels = true)
	{
		DialoguePanel panel = SwitchToPanel<DialoguePanel>();
		panel.SetDialogue(icon, charname, text);
	}

	public void InitializeAndShowTradePanel(bool hideOtherPanels = true)
	{
		TradePanel panel = SwitchToPanel<TradePanel>();
		panel.SetupTradePanel((currentInteractingObject as Prisoner).inventory, Director.Instance.GetPlayerInventory());
	}

	public void InitializeAndShowPlayerInventoryPanel(bool hideOtherPanels = false)
	{
		PlayerInventoryPanel panel = SwitchToPanel<PlayerInventoryPanel>();
		panel.Initialize();
	}

	public void InitializeAndShowCollectItemPanel(CollectibleItem item)
	{
		CollectItemPanel panel = SwitchToPanel<CollectItemPanel>();
		panel.InitializeAndShow($"You found <color=green>{item.ItemName}</color>");
	}
	public void HideAllPanels()
	{
		hudScreenPanels.ForEach(panel => panel.gameObject.SetActive(false));
	}

	public void OnTradeButtonClicked()
	{
		InitializeAndShowTradePanel();
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
}
