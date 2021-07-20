using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolPathIterator : IEnumerator<PatrolWaypoint>
{
	PatrolPath patrolPath;
	int currentIndex;
	bool isReverting;
	public PatrolPathIterator(PatrolPath path)
	{
		patrolPath = path;
		currentIndex = 0;
	}

	public PatrolWaypoint Current => patrolPath.GetWaypoints()[currentIndex];

	object IEnumerator.Current => Current;

	public void Dispose()
	{
	}

	public bool MoveNext()
	{
		PatrolWaypoint[] waypoints = patrolPath.GetWaypoints();
		if (currentIndex == 0)
		{
			isReverting = false;
			currentIndex = Mathf.Min(waypoints.Length, currentIndex + 1);
		}
		else if (currentIndex == waypoints.Length - 1)
		{
			isReverting = true;
			currentIndex = Mathf.Max(0, currentIndex - 1);
		}
		else
		{
			currentIndex = isReverting ? currentIndex - 1 : currentIndex + 1;
		}
		return true;
	}

	public void Reset()
	{
		currentIndex = 0;
	}
}