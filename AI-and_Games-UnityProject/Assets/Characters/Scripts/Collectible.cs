using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventorys;
public class Collectible : MonoBehaviour, IInteractable
{
	public CollectibleItem collectibleItem;
	[SerializeField] public float interactRange;
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
		if (initiater.GetComponent<Player>())
		{
			Debug.Log("Player found me!");
			AudioManager.Instance.PlaySFX(Director.Instance.audioDataBase.collectObjectClip);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, interactRange);
	}
}
