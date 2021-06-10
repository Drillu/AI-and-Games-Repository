using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HudScreenPanel : MonoBehaviour
{
	protected HudScreen hudScreen;
	protected HudScreen.PanelType panelType;
	public virtual void Initialize(HudScreen hud)
	{
		hudScreen = hud;
	}
}
