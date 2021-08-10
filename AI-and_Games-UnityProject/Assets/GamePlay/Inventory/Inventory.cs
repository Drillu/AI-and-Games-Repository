using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventorys
{
	public enum InventoryItemType
	{
		Default,
		Soap,
		Toothpaste,
		Book,
		ToiletPaper,
		Apple,
		ScrewDriver,
		Spoon,
		ChewingGum,
		PaperAndPencil
	}
	[System.Serializable]
	public class Inventory
	{
		public List<InventoryItem> inventoryItems;
		public GameObject owner;
		public Inventory()
		{
			inventoryItems = new List<InventoryItem>();
		}
		public Inventory(GameObject owner)
		{
			inventoryItems = new List<InventoryItem>();
			this.owner = owner;
		}

		public void TradeForItemInOtherInventory(InventoryItem targetItem, Inventory otherInventory)
		{
			if(otherInventory.ContainsItem(targetItem))
			{
				if(targetItem.CanTrade(this))
				{
					// reduce recipe
					foreach(RecipeItem item in targetItem.Recipe)
					{
						inventoryItems.Find(e => e.type == item.type).amount -= item.amount;
					}
					// remove item from other inventory
					otherInventory.RemoveItem(targetItem);
					// add item to this inventory
					AddItem(targetItem);
				}
			}
		}

		public bool ContainsItem(InventoryItem item)
		{
			return inventoryItems.Find(e => e.type == item.type) != null;
		}

		public bool ContainsItem(InventoryItemType itemType)
		{
			return inventoryItems.Find(e => e.type == itemType) != null;
		}

		public InventoryItem AddItem(InventoryItem item)
		{
			InventoryItem itm = inventoryItems.Find(e => e.type == item.type && e.isHoldForPlayer == item.isHoldForPlayer);
			if(itm != null)
			{
				itm.amount += 1;
				return itm;
			}
			else
			{
				InventoryItem newItem = new InventoryItem(item);
				inventoryItems.Add(newItem);
				return newItem;
			}
		}

		public InventoryItem GetItem(InventoryItemType itemType)
		{
			return inventoryItems.Find(e => e.type == itemType);
		}

		public void RemoveItem(InventoryItem item)
		{
			InventoryItem itm = inventoryItems.Find(e => e.type == item.type && e.isHoldForPlayer == item.isHoldForPlayer);
			itm.amount -= 1;
			if(itm.amount <= 0)
			{
				inventoryItems.Remove(itm);
			}
		}
		public void RemoveItem(InventoryItemType itemType)
		{
			InventoryItem itm = inventoryItems.Find(e => e.type == itemType);
			itm.amount -= 1;
			if(itm.amount <= 0)
			{
				inventoryItems.Remove(itm);
			}
		}

		public void RemoveAllItems()
		{
			inventoryItems.Clear();
		}

		public void MergeItemOfSameType()
		{
			Dictionary<InventoryItemType, int> typeIndex = new Dictionary<InventoryItemType, int>();
			for(int i = 0; i < inventoryItems.Count; i++)
			{
				InventoryItem item = inventoryItems[i];
				if(typeIndex.ContainsKey(item.type))
				{
					inventoryItems[typeIndex[item.type]].amount += item.amount;
					item.amount = 0;
				}
				else
				{
					typeIndex[item.type] = i;
				}
				item.isHoldForPlayer = false;
			}

			inventoryItems = inventoryItems.FindAll(e => e.amount > 0);
		}
	}
}
