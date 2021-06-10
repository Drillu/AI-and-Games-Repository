using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
	[SerializeField] float gizmosRadius = 0.5f;
	private PatrolWaypoint[] waypoints;
	private void Start()
	{
		waypoints = GetComponentsInChildren<PatrolWaypoint>();
	}




	private void OnDrawGizmos()
	{
		PatrolWaypoint[] gizmosWaypoints = GetComponentsInChildren<PatrolWaypoint>();
		for (int i = 0; i < gizmosWaypoints.Length; i++)
		{
			Gizmos.color = i == 0 ? Color.cyan : Color.white;
			Gizmos.DrawSphere(gizmosWaypoints[i].transform.position, gizmosRadius);

			int next = i == gizmosWaypoints.Length - 1 ? 0 : i + 1;
			Gizmos.DrawLine(gizmosWaypoints[i].transform.position, gizmosWaypoints[next].transform.position);
		}
	}
}