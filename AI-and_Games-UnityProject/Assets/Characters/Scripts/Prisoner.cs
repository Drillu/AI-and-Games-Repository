using System.Collections;
using System.Collections.Generic;
using Inventorys;
using UnityEngine;

public class Prisoner : Agent, IInteractable
{
	[Header("Config")]
	public float talkRange = 3f;
	public float patrolSpeed;
	[SerializeField] PatrolPath patrolPath;
	public bool panicOnPrequisition;
	[Header("Speach")]
	public List<DialoguePool> dialoguePool;
	public List<string> Speach {
		get
		{
			int randomInd = Random.Range(0, dialoguePool.Count);
			return dialoguePool[randomInd].dialogue;
		}
	}
	[Header("Inventory")]
	public Inventory inventory = new Inventory();
	AgentMovingBehavior Behavior;
	private void Awake()
	{
		inventory.owner = this.gameObject;
		if(!Behavior)
		{
			Behavior = gameObject.AddComponent<PatrolBehavior>();
		}
	}

	private void Start()
	{
		(Behavior as PatrolBehavior).SetPatrolPath(patrolPath);
		(Behavior as PatrolBehavior).patrolSpeed = patrolSpeed;
	}

	private void Update()
	{
		Behavior?.Act();
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

	public void AssignInventory(Inventory newInventory){
		inventory = newInventory;
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
		result.Add(new RecipeItem(InventoryItemType.ChewingGum, 1));
		return result;
		void AddItemToResult(RecipeItem item)
		{
			if(result.Find(e => e.type == item.type) != null)
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
