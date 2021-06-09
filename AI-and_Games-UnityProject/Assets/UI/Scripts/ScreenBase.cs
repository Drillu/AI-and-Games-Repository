using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScreenBase : MonoBehaviour
{
    public UIManager.ScreenType screenType;
    public abstract void Initialize();
}
