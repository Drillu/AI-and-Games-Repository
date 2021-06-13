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
	public float GetInteractRange()
	{
		return talkRange;
	}

	public override Inventory GetInventory()
	{
		return inventory;
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}

	public void Interact(GameObject initiater)
	{
		Director.Instance.TalkToPrisoner(this);
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, talkRange);
	}
}
