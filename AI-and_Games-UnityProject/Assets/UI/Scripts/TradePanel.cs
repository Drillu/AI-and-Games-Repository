using System;
using System.Collections.Generic;
using System.Text;
using Inventorys;
using UnityEngine;

public class TradePanel : HudScreenPanel
{
	[SerializeField] InventoryPanel firstInventoryPanel;
	[SerializeField] InventoryPanel secondInventoryPanel;

	Inventory prisonerInventory;
	Inventory playerInventory;

	public override bool ListenToInput()
	{
		return true;
	}

	public void OnCancelPressed()
	{

	}

	public void SetupTradePanel(Inventory first, Inventory second)
	{
		prisonerInventory = first;
		playerInventory = second;

		firstInventoryPanel.SetupInventoryPanel(first, OnPrisonerItemHovered, OnPrisonerItemClicked);
		firstInventoryPanel.gameObject.SetActive(true);
		secondInventoryPanel.SetupInventoryPanel(second, OnPlayerItemHovered, OnPlayerItemClicked);
		secondInventoryPanel.gameObject.SetActive(true);
	}
	public void OnPrisonerItemHovered(Inventory inventory, InventoryItem item, InventoryPanel panel)
	{
		panel.SetItemDescription(ConstructPrisonerItemDescription(item));
	}
	public void OnPrisonerItemClicked(Inventory inventory, InventoryItem item, InventoryPanel panel)
	{
		if (item.CanTrade(playerInventory))
		{
			playerInventory.TradeForItemInOtherInventory(item, inventory);
			// item.isHoldForPlayer = false;
			RefreshPanel();
		}
		else
		{
			panel.SetItemDescription("Man you don't have enough item to trade that!");
		}
	}
	public void OnPlayerItemHovered(Inventory inventory, InventoryItem item, InventoryPanel panel)
	{
		panel.SetItemDescription(ConstructPlayerItemDescription(item) + "Should I let him hold this item for me temporarily..?");
	}
	public void OnPlayerItemClicked(Inventory inventory, InventoryItem item, InventoryPanel panel)
	{
		inventory.RemoveItem(item);
		prisonerInventory.AddItem(item);
		InventoryItem itemInPrisoner = prisonerInventory.GetItem(item.type);
		itemInPrisoner.isHoldForPlayer = true;
		itemInPrisoner.Recipe = Prisoner.GetPriceForHoldingItemForPlayer();
		RefreshPanel();
	}

	void RefreshPanel()
	{
		firstInventoryPanel.SetupInventoryPanel(prisonerInventory, OnPrisonerItemHovered, OnPrisonerItemClicked);
		secondInventoryPanel.SetupInventoryPanel(playerInventory, OnPlayerItemHovered, OnPlayerItemClicked);
	}

	public string ConstructPrisonerItemDescription(InventoryItem item)
	{

		if (item.Recipe != null && item.Recipe.Count > 0)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(item.isHoldForPlayer ? "You waanna get it back with " : $"You wanna trade {item.name} with ");
			for (int i1 = 0; i1 < item.Recipe.Count; i1++)
			{
				RecipeItem i = item.Recipe[i1];
				sb.Append(i.amount + " " + i.name);
				if (i1 != item.Recipe.Count - 1)
				{
					sb.Append(", ");
				}
				else
				{
					sb.Append("?");
				}
			}
			return sb.ToString();
		}
		else
		{
			return "Lucky man, you can get this for FREE!";
		}
	}
	public string ConstructPlayerItemDescription(InventoryItem item)
	{
		if (string.IsNullOrEmpty(item.description))
		{
			return "This is an misterous item... I don't know what it is.";
		}
		else
		{
			return item.description;
		}
	}
}