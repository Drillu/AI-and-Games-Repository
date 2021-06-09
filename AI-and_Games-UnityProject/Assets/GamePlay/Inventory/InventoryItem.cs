using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventorys
{
    [System.Serializable]
	public class InventoryItem
	{
		public InventoryItemType type;
		public int amount;
		public Dictionary<InventoryItem, int> Recipe;
		public InventoryItem()
		{
			type = InventoryItemType.Default;
			amount = 1;
			Recipe = new Dictionary<InventoryItem, int>();
		}
		public InventoryItem(InventoryItem other)
		{
			type = other.type;
			amount = 1;
			Recipe = other.Recipe;
		}

		public bool CanTrade(Inventory inventory)
		{
			foreach (InventoryItem item in Recipe.Keys)
			{
				InventoryItem recepiItem = inventory.inventoryItems.Find(e => e.type == item.type);
				if (recepiItem == null || recepiItem.amount < Recipe[item])
				{
					return false;
				}
			}
			return true;
		}
	}
}
