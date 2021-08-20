using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScreenBase : MonoBehaviour
{
	[SerializeField] GameObject ScreenRoot;
	public UIManager.ScreenType screenType;
	public abstract void Initialize();
	/// <summary>
	/// Return True if after the input, current screen still the active screen
	/// Return False if after the input, current screen quits
	/// </summary>
	/// <returns></returns>
	public abstract bool ListenToInput();
	public virtual void Hide()
	{
		ScreenRoot.SetActive(false);
	}
	public bool IsCurrentActiveScreen;
}
