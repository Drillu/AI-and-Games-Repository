using System;
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
	[SerializeField] LayerMask chasingTargetLayer;
	AgentMovingBehavior Behavior;



	// Start is called before the first frame update
	void Start()
	{
		GameEvents.OnPrequisitionStart.AddListener(OnPrequisitionStart);
		GameEvents.OnPrequisitionEnd.AddListener(OnPrequisitionEnd);
		if (!gameObject.GetComponent<GuardPatrolBehaviour>())
		{
			Behavior = gameObject.AddComponent<GuardPatrolBehaviour>();
		}

		(Behavior as GuardPatrolBehaviour).SetPatrolPath(patrolPath);
		(Behavior as GuardPatrolBehaviour).patrolSpeed = patrolSpeed;
		(Behavior as GuardPatrolBehaviour).chasingRange = giveupRange;
		(Behavior as GuardPatrolBehaviour).chasingTargetLayer = chasingTargetLayer;
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

	public void SwitchToPatrolBehaviour()
	{
		Behavior = gameObject.AddOrGetComponent<GuardPatrolBehaviour>();
		(Behavior as GuardPatrolBehaviour).patrolSpeed = patrolSpeed;
	}

	public void SwitchToPrequisitionBehaviour(Agent player)
	{
		Behavior = gameObject.AddOrGetComponent<GuardPrequisitionBehaviour>();
		(Behavior as GuardPrequisitionBehaviour).Initialize(player, giveupRange, chasingSpeed);
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
