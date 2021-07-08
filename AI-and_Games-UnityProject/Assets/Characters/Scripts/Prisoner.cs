using System.Collections;
using System.Collections.Generic;
using Inventorys;
using UnityEngine;

public class Prisoner : Agent, IInteractable
{
	public string Speach = "";
	public string Name = "";
	public Inventory inventory = new Inventory();
	public float talkRange = 3f;
	private void Awake()
	{
		inventory.owner = this.gameObject;
	}
	public float GetInteractRange()
	{
		return talkRange;
	}

	public override Inventory GetInventory()
	{
		return inventory;
	}

	public Vector3 GetInteractCenter()
	{
		return transform.position;
	}

	public void Interact(GameObject initiater)
	{
		AudioManager.Instance.PlaySFX(Director.Instance.audioDataBase.GetRandomChatSFXClip());
		Director.Instance.TalkToPrisoner(this);
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, talkRange);
	}

	public static List<RecipeItem> GetPriceForHoldingItemForPlayer()
	{
		int no = Random.Range(1, 3);
		List<RecipeItem> result = new List<RecipeItem>();
		RecipeItem r;
		for (int i = 0; i < no; i++)
		{
			int itemI = Random.Range(0, 5);
			switch (itemI)
			{
				case 1:
					r = new RecipeItem();
					r.type = InventoryItemType.Soap;
					r.amount = 1;
					AddItemToResult(r);
					break;
				case 2:
					r = new RecipeItem();
					r.type = InventoryItemType.Toothpaste;
					r.amount = 1;
					AddItemToResult(r);
					break;
				case 3:
					r = new RecipeItem();
					r.type = InventoryItemType.ToiletPaper;
					r.amount = 1;
					AddItemToResult(r);
					break;
				case 4:
					r = new RecipeItem();
					r.type = InventoryItemType.Apple;
					r.amount = 1;
					AddItemToResult(r);
					break;
				default:
					break;
			}
		}
		return result;
		void AddItemToResult(RecipeItem item)
		{
			if (result.Find(e => e.type == item.type) != null)
			{
				result.Find(e => e.type == item.type).amount++;
			}
			else
			{
				result.Add(item);
			}
		}
	}
}
