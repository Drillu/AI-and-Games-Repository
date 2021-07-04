using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents
{
	public static UnityEvent OnPrequisitionStart = new UnityEvent();
	public static UnityEvent OnPrequisitionEnd = new UnityEvent();
}
