using System;
using System.Collections.Generic;
using System.Text;
using Inventorys;
using UnityEngine;
using UnityEngine.UI;

public class TradePanel : HudScreenPanel
{
	public enum TradeTarget
	{
		Prisoner,
		Chest
	}

	[SerializeField] Image firstInventoryIcon;
	[SerializeField] InventoryPanel firstInventoryPanel;
	[SerializeField] Image secondInventoryIcon;
	[SerializeField] InventoryPanel secondInventoryPanel;

	Inventory firstInventory;
	Inventory playerInventory;
	TradeTarget currentTradeTarget;

	public override bool ListenToInput()
	{
		if (InputManager.Instance.IsCancelButtonPressed)
		{
			OnCancelPressed();
			return false;
		}
		else
		{
			return true;
		}
	}

	public void OnCancelPressed()
	{
		firstInventory = null;
		playerInventory = null;
		gameObject.SetActive(false);
	}

	public void SetupTradePanel(Inventory first, Inventory second, TradeTarget target = TradeTarget.Prisoner)
	{
		currentTradeTarget = target;

		firstInventory = first;
		playerInventory = second;

		Sprite firstSprite = first.owner.GetComponent<IHasSpriteIcon>()?.GetIconSprite();
		firstInventoryIcon.sprite = firstSprite;
		firstInventoryIcon.gameObject.SetActive(firstSprite);
		if (currentTradeTarget == TradeTarget.Prisoner)
		{
			firstInventoryPanel.SetupInventoryPanel(first, OnPrisonerItemHovered, OnPrisonerItemClicked);
		}
		else
		{
			firstInventoryPanel.SetupInventoryPanel(first, OnChestItemHovered, OnChestItemClicked);
		}
		firstInventoryPanel.gameObject.SetActive(true);

		Sprite secondSprite = second.owner.GetComponent<IHasSpriteIcon>()?.GetIconSprite();
		secondInventoryIcon.sprite = secondSprite;
		secondInventoryIcon.gameObject.SetActive(secondSprite);
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
			playerInventory.MergeItemOfSameType();
			RefreshPanel();
		}
		else
		{
			panel.SetItemDescription("Man you don't have enough item to trade that!");
		}
	}

	public void OnChestItemHovered(Inventory inventory, InventoryItem item, InventoryPanel panel)
	{
		panel.SetItemDescription(ConstructChestItemDescription(item));
	}

	public void OnChestItemClicked(Inventory inventory, InventoryItem item, InventoryPanel panel)
	{
		inventory.RemoveItem(item);
		playerInventory.AddItem(item);
		playerInventory.MergeItemOfSameType();
		RefreshPanel();
	}

	public void OnPlayerItemHovered(Inventory inventory, InventoryItem item, InventoryPanel panel)
	{
		panel.SetItemDescription(ConstructPlayerItemDescription(item) + "Should I let him hold this item for me temporarily..?");
	}

	public void OnPlayerItemClicked(Inventory inventory, InventoryItem item, InventoryPanel panel)
	{
		inventory.RemoveItem(item);
		InventoryItem newItem = new InventoryItem(item);
		InventoryItem addedItem = firstInventory.AddItem(newItem);

		bool isTradingWithPrisoner = currentTradeTarget == TradeTarget.Prisoner;
		newItem.isHoldForPlayer = isTradingWithPrisoner;
		addedItem.isHoldForPlayer = isTradingWithPrisoner;
		if (isTradingWithPrisoner)
		{
			addedItem.Recipe = Prisoner.GetPriceForHoldingItemForPlayer();
		}

		RefreshPanel();
	}

	void RefreshPanel()
	{
		if (currentTradeTarget == TradeTarget.Prisoner)
		{
			firstInventoryPanel.SetupInventoryPanel(firstInventory, OnPrisonerItemHovered, OnPrisonerItemClicked);
		}
		else
		{
			firstInventoryPanel.SetupInventoryPanel(firstInventory, OnChestItemHovered, OnChestItemClicked);
		}
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

	public string ConstructChestItemDescription(InventoryItem item)
	{
		return $"A {item.name} that you stored in the chest.";
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