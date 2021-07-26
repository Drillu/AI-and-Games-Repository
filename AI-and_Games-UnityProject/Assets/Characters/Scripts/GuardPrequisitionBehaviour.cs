using System;
using UnityEngine;

public class GuardPrequisitionBehaviour : AgentMovingBehavior
{
	public float chasingSpeed;
	public float giveupRange;
	NavagentMover _navAgentMover;
	NavagentMover navAgentMover
	{
		get
		{
			if (!_navAgentMover)
			{
				_navAgentMover = GetComponent<NavagentMover>();
			}
			return _navAgentMover;
		}
	}
	private Agent target;
	float caughtRange;
	public void Initialize(Agent target, float giveupRange, float chasingSpeed, float caughtRange)
	{
		this.target = target;
		this.giveupRange = giveupRange;
		this.chasingSpeed = chasingSpeed;
		this.caughtRange = caughtRange;
	}

	private bool IsTargetInChasingRange()
	{
		return Vector3.Distance(target.transform.position, transform.position) <= giveupRange;
	}

	public override void Act()
	{
		if (Director.Instance.isPrequisitioning)
		{
			if (target && IsInCaughtRange())
			{
				GetComponent<Guard>().CaughtPlayer();
			}
			else if (target && IsTargetInChasingRange())
			{
				navAgentMover.SetSpeed(chasingSpeed);
				navAgentMover.MoveToPosition(target.transform.position);
			}
			else
			{
				GetComponent<Guard>().SwitchToPatrolBehaviour();
			}
		}
		else
		{
			GetComponent<Guard>().SwitchToPatrolBehaviour();
		}
	}

	private bool IsInCaughtRange()
	{
		return Vector3.Distance(target.transform.position, transform.position) <= caughtRange;
	}
}