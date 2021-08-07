using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventorys;
public class Exit : MonoBehaviour, IInteractable
{
	public enum ExitType
	{
		Toilet,
		Pipe
	}

	[System.Serializable]
	public class ExitItem
	{
		public CollectibleItem item;
		public int amount;
	}
	[SerializeField] List<ExitItem> exitItems;
	[SerializeField] List<string> exitTipText;
	public float interactRange;
	public bool unlocked;

	public ExitType exitType;


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
		if(unlocked)
		{
			if(exitType == ExitType.Toilet)
			{
				UIManager.Instance.SwitchToHudAndShowDialogue(null, null, Director.Instance.playerUnlockToiletDialogue);
			}
			else
			{
				UIManager.Instance.SwitchToHudAndShowDialogue(null, null, Director.Instance.playerUnlockPipeDialogue);
			}
		}
		else if(player)
		{
			bool canExit = PlayerCanExit(player);
			if(canExit)
			{
				Director.Instance.PlayerExit(this);
				unlocked = true;
			}
			else
			{
				UIManager.Instance.SwitchToHudAndShowDialogue(null, null, exitTipText);
			}
		}
	}

	private bool PlayerCanExit(Player player)
	{
		Inventory playerInventory = player.GetInventory();
		bool canExit = true;
		foreach(ExitItem exitItem in exitItems)
		{
			if(playerInventory.ContainsItem(exitItem.item.type))
			{
				if(playerInventory.GetItem(exitItem.item.type).amount < exitItem.amount)
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
