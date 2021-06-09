using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudScreen : ScreenBase
{
	[SerializeField] DialoguePanel DialoguePanel;
	[SerializeField] TradePanel TradePanel;

	public void Initialize()
	{
		HideAllPanels();
	}

	public void InitializeAndShowDialoguePanel(bool hideOtherPanels = true)
	{
		if (hideOtherPanels)
		{
			TradePanel.gameObject.SetActive(false);
		}
		DialoguePanel.Initialize();
		DialoguePanel.gameObject.SetActive(true);
	}

	public void InitializeAndShowTradePanel(bool hideOtherPanels = true)
	{
		if (hideOtherPanels)
		{
			DialoguePanel.gameObject.SetActive(false);
		}
		TradePanel.Initialize();
		TradePanel.gameObject.SetActive(true);
	}

	public void HideAllPanels()
	{
		TradePanel.gameObject.SetActive(true);
		DialoguePanel.gameObject.SetActive(true);
	}
}
