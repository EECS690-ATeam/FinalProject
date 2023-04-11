using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem 
{
    public ItemData itemData;
    public int stackSize;

    // constructor that creates an inventory item
    public InventoryItem(ItemData item)
    {
        itemData = item;
        AddToStack();
    }
     
    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}
