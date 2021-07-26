using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
	[SerializeField] float gizmosRadius = 0.5f;
	private PatrolWaypoint[] waypoints;
	public bool isPingpong;

	private void Start()
	{
		waypoints = GetComponentsInChildren<PatrolWaypoint>();
	}

	public PatrolWaypoint[] GetWaypoints()
	{
		return waypoints;
	}


	private void OnDrawGizmosSelected()
	{
		PatrolWaypoint[] gizmosWaypoints = GetComponentsInChildren<PatrolWaypoint>();
		for (int i = 0; i < gizmosWaypoints.Length; i++)
		{
			Gizmos.color = i == 0 ? Color.cyan : Color.white;
			Gizmos.DrawSphere(gizmosWaypoints[i].transform.position, gizmosRadius);

			int next = i == gizmosWaypoints.Length - 1 ? 0 : i + 1;
			if (i != gizmosWaypoints.Length - 1 || !isPingpong)
			{
				Gizmos.DrawLine(gizmosWaypoints[i].transform.position, gizmosWaypoints[next].transform.position);
			}
		}
	}
}
