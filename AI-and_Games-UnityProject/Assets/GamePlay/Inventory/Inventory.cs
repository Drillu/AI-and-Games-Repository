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
		Spoon
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
					foreach (RecipeItem item in targetItem.Recipe)
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

		public InventoryItem GetItem(InventoryItemType itemType)
		{
			return inventoryItems.Find(e => e.type == itemType);
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
