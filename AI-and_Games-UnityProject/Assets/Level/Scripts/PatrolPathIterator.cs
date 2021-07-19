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
		if(currentIndex == 0){
			isReverting = false;
		}
		return true;
	}

	public void Reset()
	{
		currentIndex = 0;
	}
}