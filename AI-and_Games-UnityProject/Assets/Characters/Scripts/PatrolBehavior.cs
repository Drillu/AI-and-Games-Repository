using System.Collections;
using System.Collections.Generic;
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
	[SerializeField] float tolerance = 1f;
	int currentWaypointIndex;
	bool isDweling;
	float dwelingCounter;
	public void SetPatrolPath(PatrolPath newPath)
	{
		patrolPath = newPath;
	}

	public void BeginPatrol()
	{
		InitializeVariables();
	}

	private void InitializeVariables()
	{
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
	public void StartPatrol()
	{
		if (Vector3.Distance(transform.position, currentWaypoint.transform.position) > tolerance)
		{
			navAgentMover.MoveToPosition(currentWaypoint.transform.position);
		}
		else
		{
			dwelingCounter += Time.deltaTime;
			if (dwelingCounter >= currentWaypoint.dwellingTime)
			{
				currentWaypointIndex = currentWaypointIndex + 1 >= currentPatrolWaypoints.Length ? 0 : currentWaypointIndex + 1;
				dwelingCounter = 0;
			}
		}
	}
}
