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

	public override bool ListenToInput()
	{
		if (InputManager.Instance.IsCancelButtonPressed || InputManager.Instance.IsShowInventoryButtonPressed)
		{
			OnCancelPressed();
			return false;
		}
		return true;
	}

	public void OnCancelPressed()
	{
		gameObject.SetActive(false);
	}
}
