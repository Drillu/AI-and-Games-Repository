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
	}

	[System.Serializable]
	public class InventoryItem
	{
		public CollectibleItem item;
		public InventoryItemType type { get { return item.type; } }
		public string name { get { return item.ItemName; } }
		public string description { get { return item.description; } }
		public int amount = 0;
		public bool isHoldForPlayer = false;

		public List<RecipeItem> Recipe;
		public InventoryItem()
		{
			amount = 1;
			Recipe = new List<RecipeItem>();
		}
		public InventoryItem(InventoryItem other)
		{
			item = other.item;
			amount = 1;
			Recipe = other.Recipe;
		}

		public bool CanTrade(Inventory inventory)
		{
			if (Recipe == null || Recipe.Count <= 0)
			{
				return true;
			}
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
