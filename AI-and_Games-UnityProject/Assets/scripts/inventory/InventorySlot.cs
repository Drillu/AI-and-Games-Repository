using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    public CollectibleItem item;

    public int amount;

    public GameObject ui;

    public InventorySlot(CollectibleItem item, int amount, GameObject ui)
    {
        this.item = item;
        this.amount = amount;
        this.ui = ui;
    }
}
