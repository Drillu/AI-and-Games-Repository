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

	HudScreenPanel currentActivePanel;
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

	private void ListenToInput()
	{
		if(currentActivePanel.ListenToInput()){
			
		}
		if (InputManager.Instance.IsShowInventoryButtonPressed)
		{
			InitializeAndShowPlayerInventoryPanel();
		}
	}

	public void InitializeAndShowDialoguePanel(Sprite icon, string charname, string text, bool hideOtherPanels = true)
	{
		if (hideOtherPanels)
		{
			TradePanel.gameObject.SetActive(false);
			PlayerInventoryPanel.gameObject.SetActive(false);
		}
		DialoguePanel.gameObject.SetActive(true);
		DialoguePanel.SetDialogue(icon, charname, text);
		currentActivePanel = DialoguePanel;
	}

	public void InitializeAndShowTradePanel(bool hideOtherPanels = true)
	{
		if (hideOtherPanels)
		{
			DialoguePanel.gameObject.SetActive(false);
			PlayerInventoryPanel.gameObject.SetActive(false);
		}
		TradePanel.gameObject.SetActive(true);
		currentActivePanel = TradePanel;
	}

	public void InitializeAndShowPlayerInventoryPanel(bool hideOtherPanels = false)
	{
		if (hideOtherPanels)
		{
			TradePanel.gameObject.SetActive(false);
			DialoguePanel.gameObject.SetActive(false);
		}
		PlayerInventoryPanel.Initialize();
		PlayerInventoryPanel.gameObject.SetActive(true);
		currentActivePanel = PlayerInventoryPanel;
	}

	public void HideAllPanels()
	{
		TradePanel.gameObject.SetActive(false);
		DialoguePanel.gameObject.SetActive(false);
		PlayerInventoryPanel.gameObject.SetActive(false);
	}
}
