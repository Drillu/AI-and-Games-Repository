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

	Queue<HudScreenPanel> currentActivePanels = new Queue<HudScreenPanel>();
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
		HudScreenPanel currentPanel = currentActivePanels.Peek();
		if (!currentPanel.ListenToInput())
		{
			currentActivePanels.Dequeue();
		}
		return currentActivePanels.Count > 0;
		// if (InputManager.Instance.IsShowInventoryButtonPressed)
		// {
		// 	InitializeAndShowPlayerInventoryPanel();
		// }
	}

	public void HudScreenPanelActionDone()
	{
		currentActivePanels.Dequeue();
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
		currentActivePanels.Enqueue(DialoguePanel);
	}

	public void InitializeAndShowTradePanel(bool hideOtherPanels = true)
	{
		if (hideOtherPanels)
		{
			DialoguePanel.gameObject.SetActive(false);
			PlayerInventoryPanel.gameObject.SetActive(false);
			currentActivePanels.Clear();
		}
		TradePanel.gameObject.SetActive(true);
		currentActivePanels.Enqueue(TradePanel);
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
		currentActivePanels.Enqueue(PlayerInventoryPanel);
	}

	public void HideAllPanels()
	{
		TradePanel.gameObject.SetActive(false);
		DialoguePanel.gameObject.SetActive(false);
		PlayerInventoryPanel.gameObject.SetActive(false);
	}
}
