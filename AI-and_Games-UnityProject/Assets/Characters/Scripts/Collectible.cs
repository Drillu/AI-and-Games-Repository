using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventorys;
public class Collectible : MonoBehaviour, IInteractable
{
	public CollectibleItem collectibleItem;
	[SerializeField] public float interactRange;
	private System.Action<Collectible> OnItemPickedUp;
	public void Initialize(System.Action<Collectible> onPickedup)
	{
		OnItemPickedUp = onPickedup;
	}
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
			UIManager.Instance.SwitchToScreen(UIManager.ScreenType.HudScreen);
			UIManager.Instance.GetScreenComponent<HudScreen>().InitializeAndShowCollectItemPanel(collectibleItem);
			OnItemPickedUp?.Invoke(this);
			Destroy(gameObject);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, interactRange);
	}
}
