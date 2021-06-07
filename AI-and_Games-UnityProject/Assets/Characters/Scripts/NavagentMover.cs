using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavagentMover : MonoBehaviour
{
	private NavMeshAgent _navmeshAgent;
	private NavMeshAgent navMeshAgent { get { if (!_navmeshAgent) { _navmeshAgent = GetComponent<NavMeshAgent>(); } return _navmeshAgent; } }

	public void MoveToPosition(Vector3 position)
	{
		navMeshAgent.isStopped = false;
		navMeshAgent.destination = position;
	}

	public void StopMoving()
	{
		navMeshAgent.isStopped = true;
	}
}
