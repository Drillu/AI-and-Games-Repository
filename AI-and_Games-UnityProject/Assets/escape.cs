using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escape : MonoBehaviour
{
    public Item required;
    public int amount;
    public Inventory inventory;
    public float radius;
    public LayerMask PlayerLayer;
    public KeyCode dialogKey;
    [InspectorName("Layer to load onEscape"), Tooltip("build index")]
    public int layerInt;

    public void onLoot()
    {
        SceneManager.LoadScene(layerInt);
    }

    private void Update()
    {
        Collider[] player = Physics.OverlapSphere(transform.position, radius, PlayerLayer);
        if (player.Length != 0)
        {
            UIManager.Instance.AddInfo($"to escape, you have to have {amount} {required.name},\n" +
                $" once you have these, you can escape by pressing {dialogKey}");
            if (Input.GetKeyDown(dialogKey))
            {
                onLoot();
            }

        }
    }
}
