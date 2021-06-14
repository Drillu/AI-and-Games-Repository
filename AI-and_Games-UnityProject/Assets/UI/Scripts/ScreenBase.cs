using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScreenBase : MonoBehaviour
{
    [SerializeField] GameObject ScreenRoot;
	public UIManager.ScreenType screenType;
	public abstract void Initialize();
	public virtual void Hide()
	{
		ScreenRoot.SetActive(false);
	}
	public bool IsCurrentActiveScreen;
}
