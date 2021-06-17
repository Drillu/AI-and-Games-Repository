using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventorys
{
	[System.Serializable]
	public class RecipeItem
	{
		public InventoryItemType type;
		public int amount;
	}

	[System.Serializable]
	public class InventoryItem
	{
		public InventoryItemType type;
		public string name
		{
			get
			{
				string result = this.type.ToString();
				int len = result.Length;
				for (int i = 0; i < len; i++)
				{
					char c = result[i];
					if (char.IsUpper(c))
					{
						result = result.Insert(i, " ");
						i++;
					}
				}
				return result;
			}
		}
		public int amount = 0;
		public bool isHoldForPlayer = false;

		public List<RecipeItem> Recipe;
		public InventoryItem()
		{
			type = InventoryItemType.Default;
			amount = 1;
			Recipe = new List<RecipeItem>();
		}
		public InventoryItem(InventoryItem other)
		{
			type = other.type;
			amount = 1;
			Recipe = other.Recipe;
		}

		public bool CanTrade(Inventory inventory)
		{
			foreach (RecipeItem item in Recipe)
			{
				InventoryItemType t = item.type;
				InventoryItem recepiItem = inventory.inventoryItems.Find(e => e.type == t);
				if (recepiItem == null || recepiItem.amount < item.amount)
				{
					return false;
				}
			}
			return true;
		}
	}
}
