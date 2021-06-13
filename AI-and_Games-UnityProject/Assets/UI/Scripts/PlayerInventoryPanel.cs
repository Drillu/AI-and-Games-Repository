using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryPanel : HudScreenPanel
{
	[SerializeField] InventoryPanel inventoryPanel;
	public void Initialize()
	{
		inventoryPanel.SetupInventoryPanel(Director.Instance.GetPlayerInventory());
		inventoryPanel.gameObject.SetActive(true);
	}
	public void OnCancelPressed()
	{

	}
}
