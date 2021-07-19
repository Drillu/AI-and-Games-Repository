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
	public bool IsLost;

	public void SetTarget(Agent target)
	{
		this.target = target;
	}

	private bool IsTargetInChasingRange()
	{
		return Vector3.Distance(target.transform.position, transform.position) <= giveupRange;
	}

	public override void Act()
	{
		if (target)
		{
			if (IsTargetInChasingRange())
			{
				navAgentMover.SetSpeed(chasingSpeed);
				navAgentMover.MoveToPosition(target.transform.position);
				IsLost = false;
			}
			else
			{
				IsLost = true;
			}
		}
		else
		{
			IsLost = true;
		}
	}
}