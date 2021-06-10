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

	PanelType currentActivePanel;
	public override void Initialize()
	{
		HideAllPanels();
		DialoguePanel.Initialize(this);
		TradePanel.Initialize(this);
		PlayerInventoryPanel.Initialize(this);
	}

	private void Update()
	{
		if (isActiveAndEnabled)
		{
			if (InputManager.Instance.IsCancelButtonPressed)
			{
				Debug.Log("ESCPressed");
				if (currentActivePanel == PanelType.Dialogue)
				{
					DialoguePanel.OnCancelPressed();
				}
				else if (currentActivePanel == PanelType.Trade)
				{
					TradePanel.OnCancelPressed();
				}
				else if (currentActivePanel == PanelType.PlayerInventory)
				{
					PlayerInventoryPanel.OnCancelPressed();
				}
			}
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
		currentActivePanel = PanelType.Dialogue;
	}

	public void InitializeAndShowTradePanel(bool hideOtherPanels = true)
	{
		if (hideOtherPanels)
		{
			DialoguePanel.gameObject.SetActive(false);
			PlayerInventoryPanel.gameObject.SetActive(false);
		}
		TradePanel.gameObject.SetActive(true);
		currentActivePanel = PanelType.Trade;
	}

	public void InitializeAndShowPlayerInventoryPanel(bool hideOtherPanels = true)
	{
		if (hideOtherPanels)
		{
			TradePanel.gameObject.SetActive(false);
			DialoguePanel.gameObject.SetActive(false);
		}
		PlayerInventoryPanel.gameObject.SetActive(true);
		currentActivePanel = PanelType.PlayerInventory;
	}

	public void HideAllPanels()
	{
		TradePanel.gameObject.SetActive(false);
		DialoguePanel.gameObject.SetActive(false);
		PlayerInventoryPanel.gameObject.SetActive(false);
	}
}
