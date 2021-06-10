using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventorys
{
	public enum InventoryItemType
	{
		Default
	}
	[System.Serializable]
	public class Inventory
	{
		public List<InventoryItem> inventoryItems;
		public Inventory()
		{
			inventoryItems = new List<InventoryItem>();
		}

		public void TradeForItemInOtherInventory(InventoryItem targetItem, Inventory otherInventory)
		{
			if (otherInventory.ContainsItem(targetItem))
			{
				if (targetItem.CanTrade(this))
				{
					// reduce recipe
					foreach (var item in targetItem.Recipe.Keys)
					{
						inventoryItems.Find(e => e.type == item.type).amount -= targetItem.Recipe[item];
					}
					// remove item from other inventory
					otherInventory.RemoveItem(targetItem);
					// add item to this inventory
					inventoryItems.Add(targetItem);
				}
			}
		}

		public bool ContainsItem(InventoryItem item)
		{
			return inventoryItems.Find(e => e.type == item.type) != null;
		}

		public void AddItem(InventoryItem item)
		{
			InventoryItem itm = inventoryItems.Find(e => e.type == item.type);
			if (itm != null)
			{
				itm.amount += 1;
			}
			else
			{
				inventoryItems.Add(new InventoryItem(item));
			}
		}

		public void RemoveItem(InventoryItem item)
		{
			InventoryItem itm = inventoryItems.Find(e => e.type == item.type);
			itm.amount -= 1;
			if (itm.amount <= 0)
			{
				inventoryItems.Remove(itm);
			}
		}
	}
}