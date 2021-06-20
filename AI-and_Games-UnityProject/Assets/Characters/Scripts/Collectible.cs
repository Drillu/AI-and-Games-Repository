using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventorys;
public class Collectible : MonoBehaviour, IInteractable
{
	public InventoryItem collectibleItem;
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
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, interactRange);
	}
}
