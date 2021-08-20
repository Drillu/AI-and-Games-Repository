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

		if(player)
		{
			if(!unlocked)
			{
				bool canExit = PlayerCanExit(player);
				if(canExit)
				{
					unlocked = true;
					Director.Instance.PlayerExit(this);
				}
				else
				{
					UIManager.Instance.SwitchToHudAndShowDialogue(player.GetIconSprite(), player.GetAgentName(), exitTipText);
				}
			}
			else
			{
				Director.Instance.PlayerExit(this);
				//if(exitType == ExitType.Toilet)
				//{
				//	UIManager.Instance.SwitchToHudAndShowDialogue(player.GetIconSprite(), player.GetAgentName(), Director.Instance.playerUnlockToiletDialogue);
				//}
				//else
				//{
				//	UIManager.Instance.SwitchToHudAndShowDialogue(player.GetIconSprite(), player.GetAgentName(), Director.Instance.playerUnlockPipeDialogue);
				//}
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

		if(canExit)
		{
			foreach(ExitItem exitItem in exitItems)
			{
				player.GetInventory().RemoveItem(exitItem.item.type);
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
