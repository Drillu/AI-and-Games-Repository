using System.Collections;
using System.Collections.Generic;
using Inventorys;
using UnityEngine;

public class Guard : Agent
{
	[SerializeField] float patrolSpeed;
	[SerializeField] float chasingSpeed;
	[SerializeField] float giveupRange;

	[SerializeField] PatrolPath patrolPath;
	[SerializeField] float tolerance;
	AgentMovingBehavior Behavior;


	// Start is called before the first frame update
	void Start()
	{
		GameEvents.OnPrequisitionStart.AddListener(OnPrequisitionStart);
		GameEvents.OnPrequisitionEnd.AddListener(OnPrequisitionEnd);
		if (!gameObject.GetComponent<PatrolBehavior>())
		{
			Behavior = gameObject.AddComponent<PatrolBehavior>();
		}

		(Behavior as PatrolBehavior).SetPatrolPath(patrolPath);
	}

	private void OnDisable()
	{
		GameEvents.OnPrequisitionStart.RemoveListener(OnPrequisitionStart);
		GameEvents.OnPrequisitionEnd.RemoveListener(OnPrequisitionEnd);
	}

	// Update is called once per frame
	void Update()
	{
		Behavior?.Act();
	}

	public void OnPrequisitionStart()
	{

	}
	public void OnPrequisitionEnd()
	{

	}

	private void StartPrequisition()
	{

	}
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, giveupRange);
	}
	public override Inventory GetInventory()
	{
		return null;
	}
}
