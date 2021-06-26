using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventorys;
public class Collectible : MonoBehaviour, IInteractable
{
	public CollectibleItem collectibleItem;
	[SerializeField] public float interactRange;
	public float GetInteractRange()
	{
		return interactRange;
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}

	public void Interact(GameObject initiater)
	{
		Player player = initiater.GetComponent<Player>();
		if (player)
		{
			Debug.Log("Player found me!");
			Inventory playerInventory = player.GetInventory();
			InventoryItem item = new InventoryItem();
			item.item = collectibleItem;
			item.isHoldForPlayer = false;
			playerInventory.AddItem(item);
			AudioManager.Instance.PlaySFX(Director.Instance.audioDataBase.collectObjectClip);
			gameObject.SetActive(false);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, interactRange);
	}
}
