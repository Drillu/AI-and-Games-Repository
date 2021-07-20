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

	public void Initialize(Agent target, float giveupRange, float chasingSpeed)
	{
		this.target = target;
		this.giveupRange = giveupRange;
		this.chasingSpeed = chasingSpeed;
	}

	private bool IsTargetInChasingRange()
	{
		return Vector3.Distance(target.transform.position, transform.position) <= giveupRange;
	}

	public override void Act()
	{
		if (target && IsTargetInChasingRange())
		{
			navAgentMover.SetSpeed(chasingSpeed);
			navAgentMover.MoveToPosition(target.transform.position);
		}
		else
		{
			GetComponent<Guard>().SwitchToPatrolBehaviour();
		}
	}
}