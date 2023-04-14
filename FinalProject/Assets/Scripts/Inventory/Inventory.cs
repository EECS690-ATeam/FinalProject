using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Doesn't need to be monobehavior
// Here I have it assigned because we can assign events to it
public class Inventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChange;

    // need list of inventory items
    public List<InventoryItem> inventory = new  List<InventoryItem>();
    // dictionary to handle stacking items
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    private void OnEnable()
    {
        Keycard.OnKeycardCollected += Add;
        Battery.OnBatteryCollected += Add;
        Syringe.OnSyringeCollected += Add;
        Tourniquet.OnTourniquetCollected += Add;
    }

    private void OnDisable()
    {
        Keycard.OnKeycardCollected -= Add;
        Battery.OnBatteryCollected -= Add;
        Syringe.OnSyringeCollected -= Add;
        Tourniquet.OnTourniquetCollected -= Add;
    }

    public void Add(ItemData itemData)
    {
        // Check if item exists
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
            Debug.Log($"{item.itemData.displayName} total stack is now {item.stackSize}");
            OnInventoryChange?.Invoke(inventory);
        }
        else
        {
            // create new inventory item
            InventoryItem newItem = new InventoryItem(itemData);
            // store in list
            inventory.Add(newItem);
            // store in dictionary
            itemDictionary.Add(itemData, newItem);
            Debug.Log($"Added {itemData.displayName} to the inventory for the first time.");
            OnInventoryChange?.Invoke(inventory);
        }
    }

    public void Remove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();

            if(item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
            OnInventoryChange?.Invoke(inventory);
        }
    }
}
