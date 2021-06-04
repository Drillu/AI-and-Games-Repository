using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public List<LootableItem> items;
    public Inventory inventory;
    public bool destroyOnLoot;
    public float radius;
    public LayerMask PlayerLayer;
    private bool looted;
    public KeyCode dialogKey;

    public void onLoot()
    {
        UIManager.Instance.AddInfo("looted");
        if (looted == false)
        {
            foreach (LootableItem i in items)
            {
                if(Chance(i.probability))
                {
                    inventory.AddItem(i.item, i.amount);
                }
            }   
        }
        looted = true;
        if(destroyOnLoot)
        {
            Destroy(gameObject);
        }
    }

    private bool Chance(float probability)
    {
        if(Random.Range(0, 100) < probability)
        {
            return true;
        }
        return false;
    }
    private void Update()
    {
        Collider[] player = Physics.OverlapSphere(transform.position, radius, PlayerLayer);
        if (player.Length != 0)
        {
            UIManager.Instance.AddInfo($"Press {dialogKey} to loot {gameObject.name}");
            if (Input.GetKeyDown(dialogKey))
            {
                onLoot();
            }
            
        }
    }
}
