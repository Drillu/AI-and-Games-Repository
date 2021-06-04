using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int slotCount = 10;

    public List<InventorySlot> slots = new List<InventorySlot>();

    public Transform slotUIRoot;

    public GameObject slotUIPrefab;

    private void OnValidate()
    {
        if (slotCount < 0)
            throw new ArgumentException("slotCount nem lehet kisebb, mint 0");
    }


    // 0 -> minden sikerult
    // -1 -> hiba
    // 1..n -> ennyi darab nem fert be
    public int AddItem(Item item, int amount
        = 1)
    {
        foreach (var slot in slots)
        {
            if (slot.item.ID == item.ID && slot.amount < item.stackSize)
            {
                int add = Mathf.Min(amount, item.stackSize - slot.amount);
                slot.amount += add;
                amount -= add;
                slot.ui.GetComponentInChildren<TMP_Text>().text = "" + slot.amount;
            }

            if (amount == 0) break;
        }

        while (slots.Count < slotCount && amount > 0)
        {
            var instance = Instantiate(slotUIPrefab, slotUIRoot, false);
            instance.GetComponent<Image>().sprite = item.itemIcon;
            instance.GetComponentInChildren<TMP_Text>().text = "" + amount;

            int add = Mathf.Min(amount, item.stackSize);
            var slot = new InventorySlot(item, add, instance);
            amount -= add;
            slots.Add(slot);
        }

        return amount;
    }

    public int RemoveItem(Item item, int amount)
    {
        var slotsToUpdate = new List<InventorySlot>();

        int currentAmount = amount;
        foreach (var slot in slots)
        {
            if (slot.item.ID == item.ID)
            {
                slotsToUpdate.Add(slot);

                int remove = Mathf.Min(slot.amount, currentAmount);
                currentAmount -= remove;

                if (currentAmount == 0) break;
            }
        }

        if (currentAmount == 0)
        {
            var slotsToRemove = new List<InventorySlot>();

            foreach (var slot in slotsToUpdate)
            {
                int remove = Mathf.Min(slot.amount, amount);
                slot.amount -= remove;
                amount -= remove;
                slot.ui.GetComponentInChildren<TMP_Text>().text = "" + slot.amount;

                if (slot.amount == 0)
                {
                    slotsToRemove.Add(slot);
                }
            }

            foreach (var slot in slotsToRemove)
            {
                Destroy(slot.ui);
                slots.Remove(slot);
            }

            return 0;
        }
        else
        {
            return currentAmount;
        }
    }
}
