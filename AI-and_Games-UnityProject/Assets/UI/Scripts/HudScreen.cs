using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudScreen : ScreenBase
{
	public enum PanelType
	{
		None,
		Dialogue,
		Trade,
		PlayerInventory
	}

	[SerializeField] DialoguePanel DialoguePanel;
	[SerializeField] TradePanel TradePanel;
	[SerializeField] PlayerInventoryPanel PlayerInventoryPanel;
	public IInteractable currentInteractingObject;
	Stack<HudScreenPanel> currentActivePanels = new Stack<HudScreenPanel>();
	public override void Initialize()
	{
		DialoguePanel.Initialize(this);
		TradePanel.Initialize(this);
		PlayerInventoryPanel.Initialize(this);
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

	public void HudScreenPanelActionDone()
	{
		currentActivePanels.Pop();
		if (currentActivePanels.Count <= 0)
		{
			// Hide Hud Screen
		}
	}

	public void InitializeAndShowDialoguePanel(Sprite icon, string charname, string text, bool hideOtherPanels = true)
	{
		if (hideOtherPanels)
		{
			TradePanel.gameObject.SetActive(false);
			PlayerInventoryPanel.gameObject.SetActive(false);
			currentActivePanels.Clear();
		}
		DialoguePanel.gameObject.SetActive(true);
		DialoguePanel.SetDialogue(icon, charname, text);
		currentActivePanels.Push(DialoguePanel);
	}

	public void InitializeAndShowTradePanel(bool hideOtherPanels = true)
	{
		if (hideOtherPanels)
		{
			DialoguePanel.gameObject.SetActive(false);
			PlayerInventoryPanel.gameObject.SetActive(false);
			currentActivePanels.Clear();
		}
		TradePanel.SetupTradePanel((currentInteractingObject as Prisoner).inventory, Director.Instance.GetPlayerInventory());
		TradePanel.gameObject.SetActive(true);
		currentActivePanels.Push(TradePanel);
	}

	public void InitializeAndShowPlayerInventoryPanel(bool hideOtherPanels = false)
	{
		if (hideOtherPanels)
		{
			TradePanel.gameObject.SetActive(false);
			DialoguePanel.gameObject.SetActive(false);
			currentActivePanels.Clear();
		}
		PlayerInventoryPanel.Initialize();
		PlayerInventoryPanel.gameObject.SetActive(true);
		currentActivePanels.Push(PlayerInventoryPanel);
	}

	public void HideAllPanels()
	{
		TradePanel.gameObject.SetActive(false);
		DialoguePanel.gameObject.SetActive(false);
		PlayerInventoryPanel.gameObject.SetActive(false);
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
}
