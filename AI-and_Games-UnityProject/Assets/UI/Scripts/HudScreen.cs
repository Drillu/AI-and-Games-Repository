using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudScreen : ScreenBase
{
	[SerializeField] DialoguePanel DialoguePanel;
	[SerializeField] TradePanel TradePanel;
	[SerializeField] PlayerInventoryPanel PlayerInventoryPanel;

	public override void Initialize()
	{
		HideAllPanels();
	}

	public void InitializeAndShowDialoguePanel(Sprite icon, string charname, string text, bool hideOtherPanels = true)
	{
		if (hideOtherPanels)
		{
			TradePanel.gameObject.SetActive(false);
			PlayerInventoryPanel.gameObject.SetActive(false);
		}
		DialoguePanel.Initialize();
		DialoguePanel.gameObject.SetActive(true);
		DialoguePanel.SetDialogue(icon, charname, text);
	}

	public void InitializeAndShowTradePanel(bool hideOtherPanels = true)
	{
		if (hideOtherPanels)
		{
			DialoguePanel.gameObject.SetActive(false);
			PlayerInventoryPanel.gameObject.SetActive(false);
		}
		TradePanel.Initialize();
		TradePanel.gameObject.SetActive(true);
	}

	public void InitializeAndShowPlayerInventoryPanel(bool hideOtherPanels = true)
	{
		if (hideOtherPanels)
		{
			TradePanel.gameObject.SetActive(false);
			DialoguePanel.gameObject.SetActive(false);
		}
		PlayerInventoryPanel.Initialize();
		PlayerInventoryPanel.gameObject.SetActive(true);
	}

	public void HideAllPanels()
	{
		TradePanel.gameObject.SetActive(false);
		DialoguePanel.gameObject.SetActive(false);
		PlayerInventoryPanel.gameObject.SetActive(false);
	}
}
