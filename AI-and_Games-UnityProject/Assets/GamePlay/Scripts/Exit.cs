using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventorys;
public class Exit : MonoBehaviour, IInteractable
{
	[System.Serializable]
	public class ExitItem
	{
		public CollectibleItem item;
		public int amount;
	}
	[SerializeField] List<ExitItem> exitItems;
	[SerializeField] string exitTipText;
	public float interactRange;
	public void CanExit()
	{

	}

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
			bool canExit = PlayerCanExit(player);
			if (canExit)
			{
				Director.Instance.PlayerExit(this);
			}
			else
			{
				UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
				UIManager.Instance.GetScreenComponent<HudScreen>().InitializeAndShowDialoguePanel(null, null, exitTipText);
			}
		}
	}

	private bool PlayerCanExit(Player player)
	{
		Inventory playerInventory = player.GetInventory();
		bool canExit = true;
		foreach (ExitItem exitItem in exitItems)
		{
			if (playerInventory.ContainsItem(exitItem.item.type))
			{
				if (playerInventory.GetItem(exitItem.item.type).amount < exitItem.amount)
				{
					canExit = false;
					break;
				}
			}
			else
			{
				canExit = false;
				break;
			}
		}

		return canExit;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, interactRange);
	}
}
