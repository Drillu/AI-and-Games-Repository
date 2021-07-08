using System.Collections;
using System.Collections.Generic;
using Inventorys;
using UnityEngine;

public class Player : Agent
{
	public static Player Instance { get; set; }
	[SerializeField] Inventory inventory = new Inventory();
	private void Awake()
	{
		if (Instance && Instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		inventory.owner = this.gameObject;
	}
	public override Inventory GetInventory()
	{
		return inventory;
	}
}
