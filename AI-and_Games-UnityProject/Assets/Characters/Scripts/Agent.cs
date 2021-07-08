using System.Collections;
using System.Collections.Generic;
using Inventorys;
using UnityEngine;

public abstract class Agent : MonoBehaviour, IHasSpriteIcon
{
	[SerializeField] Sprite iconSprite;
	public Sprite GetIconSprite()
	{
		return iconSprite;
	}

	public abstract Inventory GetInventory();
}
