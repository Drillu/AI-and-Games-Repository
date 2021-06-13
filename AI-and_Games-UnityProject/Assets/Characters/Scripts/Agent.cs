using System.Collections;
using System.Collections.Generic;
using Inventorys;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
	public abstract Inventory GetInventory();
}
