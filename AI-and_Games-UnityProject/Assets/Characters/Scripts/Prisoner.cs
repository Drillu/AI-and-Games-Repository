using System.Collections;
using System.Collections.Generic;
using Inventorys;
using UnityEngine;

public class Prisoner : MonoBehaviour, IInteractable
{
	public string Speach = "";
	public string Name = "";
	public Inventory prosonerInventory = new Inventory();
	public Inventory holdsForPlayerInventory = new Inventory();
	public float talkRange = 3f;
	public float GetInteractRange()
	{
		return talkRange;
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
