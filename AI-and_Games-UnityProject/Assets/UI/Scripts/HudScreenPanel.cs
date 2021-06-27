using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HudScreenPanel : MonoBehaviour
{
	protected HudScreen hudScreen;
	public virtual void Initialize(HudScreen hud)
	{
		hudScreen = hud;
	}

/// <summary>
/// 
/// </summary>
/// <returns>
/// true: Panel still active after react to input operation
/// false: Panel become inactive after react to input operation
/// </returns>
	public abstract bool ListenToInput();
}
