using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
	public void InventoryButtonPressed()
	{
		if(!Director.Instance.IsInteractingWithUI)
		{
			UIManager.Instance.ShowPlayerInventory();
		}
	}
}
