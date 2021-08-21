using System.Collections;
using System.Collections.Generic;
using Inventorys;
using UnityEngine;

public class Player : Agent
{
	[SerializeField] Inventory inventory = new Inventory();
	private void Awake()
	{
		inventory.owner = this.gameObject;
	}
	public override Inventory GetInventory()
	{
		return inventory;
	}
}
