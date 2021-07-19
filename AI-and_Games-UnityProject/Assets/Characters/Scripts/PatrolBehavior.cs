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
	PatrolWaypoint[] currentPatrolWaypoints;
	PatrolWaypoint currentWaypoint
	{
		get
		{
			return currentPatrolWaypoints[currentWaypointIndex];
		}
	}
	int currentWaypointIndex;
	bool isDweling;
	float dwelingCounter;
	public void SetPatrolPath(PatrolPath newPath)
	{
		patrolPath = newPath;
		if (patrolPath && patrolPath.GetWaypoints().Length > 0)
		{
			currentPatrolWaypoints = patrolPath.GetWaypoints();
			currentWaypointIndex = 0;
		}
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
		if (Vector3.Distance(transform.position, currentWaypoint.transform.position) > GetComponent<Agent>().GetNavTolerance())
		{
			navAgentMover.MoveToPosition(currentWaypoint.transform.position);
		}
		else
		{
			dwelingCounter += Time.deltaTime;
			if (dwelingCounter >= currentWaypoint.dwellingTime)
			{
				if (currentWaypointIndex + 1 >= currentPatrolWaypoints.Length)
				{
					currentWaypointIndex = 0;
					if (patrolPath.isPingpong)
					{
						currentPatrolWaypoints = currentPatrolWaypoints.Reverse().ToArray();
					}
				}
				else
				{
					currentWaypointIndex++;
				}
				currentWaypointIndex = currentWaypointIndex + 1 >= currentPatrolWaypoints.Length ? 0 : currentWaypointIndex + 1;
				dwelingCounter = 0;
			}
		}
	}
}
