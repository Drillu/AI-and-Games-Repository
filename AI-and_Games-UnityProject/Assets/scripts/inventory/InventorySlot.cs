using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    public Item item;

    public int amount;

    public GameObject ui;

    public InventorySlot(Item item, int amount, GameObject ui)
    {
        this.item = item;
        this.amount = amount;
        this.ui = ui;
    }
}
