using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Item itemToAdd;
    public int amountToAdd;

    public Item itemToRemove;
    public int amountToRemove;

    private Inventory inventory;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Debug.Log(inventory.AddItem(itemToAdd, amountToAdd));
        if (Input.GetKeyDown(KeyCode.Backspace)) inventory.RemoveItem(itemToRemove, amountToRemove);
    }
}
