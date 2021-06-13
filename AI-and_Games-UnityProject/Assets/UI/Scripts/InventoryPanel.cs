using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
	[SerializeField] Transform content;
	[SerializeField] GameObject inventoryItemPrefab;
	public void SetupInventoryPanel(Inventorys.Inventory inventory)
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
				newItemPrefab.GetComponent<InventoryItemButton>().Initalize(item);
				newItemPrefab.SetActive(true);
			}
		}

		for (; i < existingPrefabs.Length; i++)
		{
			existingPrefabs[i].gameObject.SetActive(false);
		}
	}
}
