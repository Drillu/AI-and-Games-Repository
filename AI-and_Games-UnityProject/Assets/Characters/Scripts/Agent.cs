using System.Collections;
using System.Collections.Generic;
using Inventorys;
using UnityEngine;

public abstract class Agent : MonoBehaviour, IHasSpriteIcon
{
	[SerializeField] Sprite iconSprite;
	[SerializeField] float navTolerance;
	public Sprite GetIconSprite()
	{
		return iconSprite;
	}
	public virtual float GetNavTolerance()
	{
		return navTolerance;
	}
	public abstract Inventory GetInventory();
}
