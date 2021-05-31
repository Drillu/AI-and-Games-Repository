using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActivateHUDScript : MonoBehaviour
{
    public GameObject hud;

    // Update is called once per frame
    public void OnOffClicked ()
    {
        if (hud.activeInHierarchy == true)
            hud.SetActive(false);
        else
            hud.SetActive(true);
    }
}
