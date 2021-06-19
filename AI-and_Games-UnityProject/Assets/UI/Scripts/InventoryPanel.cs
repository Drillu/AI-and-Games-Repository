using System;
using Inventorys;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
	[SerializeField] Transform content;
	[SerializeField] GameObject inventoryItemPrefab;
	[SerializeField] TMPro.TextMeshProUGUI itemDescriptionTmp;
	public void SetupInventoryPanel(Inventorys.Inventory inventory,
	Action<Inventory, InventoryItem, InventoryPanel> OnInventoryItemHovered = null,
	Action<Inventory, InventoryItem, InventoryPanel> OnInventoryItemClicked = null)
	{
		InventoryItemButton[] existingPrefabs = content.GetComponentsInChildren<InventoryItemButton>();

		int i = 0;
		for (; i < inventory.inventoryItems.Count; i++)
		{
			Inventorys.InventoryItem item = inventory.inventoryItems[i];
			if (i < existingPrefabs.Length)
			{
				existingPrefabs[i].Initalize(item,
				() => { OnInventoryItemHovered?.Invoke(inventory, item, this); },
				() => OnInventoryItemClicked?.Invoke(inventory, item, this));
				existingPrefabs[i].gameObject.SetActive(true);
			}
			else
			{
				GameObject newItemPrefab = Instantiate(inventoryItemPrefab, content);
				InventoryItemButton itemButton = newItemPrefab.GetComponent<InventoryItemButton>();
				itemButton.Initalize(item,
				() => { OnInventoryItemHovered?.Invoke(inventory, item, this); },
				() => OnInventoryItemClicked?.Invoke(inventory, item, this));
				newItemPrefab.SetActive(true);
			}
		}

		for (; i < existingPrefabs.Length; i++)
		{
			existingPrefabs[i].gameObject.SetActive(false);
		}
	}

	public void UpdatePanel(){

	}
	public void SetItemDescription(string desc)
	{
		itemDescriptionTmp.text = desc;
	}
}
