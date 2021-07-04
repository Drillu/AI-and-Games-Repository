using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventorys;

public class Storage : MonoBehaviour, IInteractable
{
	[SerializeField] float interactRange;
	Inventory inventory = new Inventory();
	public Vector3 GetInteractCenter()
	{
		return transform.position;
	}

	public float GetInteractRange()
	{
		return interactRange;
	}

	public void Interact(GameObject initiater)
	{
		Player player = initiater.GetComponent<Player>();
		if (player)
		{
			UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
			HudScreen hud = UIManager.Instance.GetScreenComponent<HudScreen>();
			hud.InitializeAndShowTradePanel(inventory, player.GetInventory());
		}
	}
}
