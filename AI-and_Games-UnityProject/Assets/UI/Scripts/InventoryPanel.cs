using System;
using Inventorys;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
	[SerializeField] Transform content;
	[SerializeField] GameObject inventoryItemPrefab;
	[SerializeField] TMPro.TextMeshProUGUI recipeText;
	public void SetupInventoryPanel(Inventorys.Inventory inventory, Func<Inventory, InventoryItem, Action<InventoryItem>> OnInventoryItemHovered = null, Func<Inventory, InventoryItem, Action<InventoryItem>> OnInventoryItemClicked = null)
	{
		InventoryItemButton[] existingPrefabs = content.GetComponentsInChildren<InventoryItemButton>();

		int i = 0;
		for (; i < inventory.inventoryItems.Count; i++)
		{
			Inventorys.InventoryItem item = inventory.inventoryItems[i];
			if (i < existingPrefabs.Length)
			{
				existingPrefabs[i].Initalize(item);
				existingPrefabs[i].gameObject.SetActive(true);
			}
			else
			{
				GameObject newItemPrefab = Instantiate(inventoryItemPrefab, content);
				newItemPrefab.GetComponent<InventoryItemButton>().Initalize(item, OnInventoryItemHovered?.Invoke(inventory, item), OnInventoryItemClicked?.Invoke(inventory, item));
				newItemPrefab.SetActive(true);
			}
		}

		for (; i < existingPrefabs.Length; i++)
		{
			existingPrefabs[i].gameObject.SetActive(false);
		}
	}
}
