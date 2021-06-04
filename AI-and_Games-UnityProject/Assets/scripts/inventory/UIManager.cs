using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text infoBox;

    private void Awake()
    {
        Instance = this;
    }

    public void AddInfo(string info)
    {
        infoBox.text = "latest message: " + info;
    }
    
}
