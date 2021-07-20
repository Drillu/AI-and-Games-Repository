using System.Linq;
using UnityEngine;

public class PatrolBehavior : AgentMovingBehavior
{
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
	[SerializeField] PatrolPath patrolPath;
	float dwelingCounter;
	PatrolPathIterator patrolPathIterator;
	public void SetPatrolPath(PatrolPath newPath)
	{
		patrolPath = newPath;
		patrolPathIterator = new PatrolPathIterator(patrolPath);
	}

	public void TerminatePatrol()
	{

	}

	public void PausePatrol()
	{

	}

	public void ContinuePatrol()
	{

	}

	public override void Act()
	{
		if (Vector3.Distance(transform.position, GetCurrentTargetPatrolWaypoint().transform.position) > GetComponent<Agent>().GetNavTolerance())
		{
			navAgentMover.MoveToPosition(GetCurrentTargetPatrolWaypoint().transform.position);
		}
		else
		{
			dwelingCounter += Time.deltaTime;
			if (dwelingCounter >= GetCurrentTargetPatrolWaypoint().dwellingTime)
			{
				// if (currentWaypointIndex + 1 >= currentPatrolWaypoints.Length)
				// {
				// 	currentWaypointIndex = 0;
				// 	if (patrolPath.isPingpong)
				// 	{
				// 		currentPatrolWaypoints = currentPatrolWaypoints.Reverse().ToArray();
				// 	}
				// }
				// else
				// {
				// 	currentWaypointIndex++;
				// }
				// currentWaypointIndex = currentWaypointIndex + 1 >= currentPatrolWaypoints.Length ? 0 : currentWaypointIndex + 1;
				patrolPathIterator.MoveNext();
				dwelingCounter = 0;
			}
		}
	}

	private PatrolWaypoint GetCurrentTargetPatrolWaypoint()
	{
		return patrolPathIterator.Current;
	}
}
