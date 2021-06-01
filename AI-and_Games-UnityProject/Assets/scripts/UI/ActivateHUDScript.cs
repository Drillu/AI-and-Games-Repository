using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActivateHUDScript : MonoBehaviour
{
    private Image hud;
    public GameObject inventoryBar;

    private void Start()
    {
        hud = GetComponent<Image>();
    }

    public void HUDClicked()
    {
        hud.enabled = !inventoryBar.activeInHierarchy;
    }
}
