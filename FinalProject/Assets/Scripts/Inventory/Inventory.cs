using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Doesn't need to be monobehavior
// Here I have it assigned because we can assign events to it
public class Inventory : MonoBehaviour, IDataPersistence
{
    public ItemData batteryData;
    public ItemData tourniquetData;
    public ItemData syringeData;
    public ItemData KeycardDataLvl1;
    public ItemData KeycardDataLvl2;
    public ItemData KeycardDataLvl3;
    public ItemData KeycardDataLvl4;
    public ItemData KeycardDataLvl5;
    

    public static event Action<List<InventoryItem>> OnInventoryChange;

    // need list of inventory items
    public List<InventoryItem> inventory = new  List<InventoryItem>();

    // dictionary to handle stacking items
    public Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    // Dictionary to convert complex dictionary to a simple one for saving data, i.e., one with simple types
    public Dictionary<string, int> simpleDictionary = new Dictionary<string, int>();

    public int newCount;
    public string itemName;

    public void LoadData(GameData data)
    {
        Debug.Log("Loading data...");
        foreach (KeyValuePair<string, int> pair in data.simpleDictionary)
        {
            if(pair.Key == "Battery")
            {
                for (int i=0; i< pair.Value; i++)
                {
                    Add(batteryData);
                }
            }
            if (pair.Key == "Health Syringe")
            {
                for (int i = 0; i < pair.Value; i++)
                {
                    Add(syringeData);
                }
            }
            if (pair.Key == "Torniquet")
            {
                for (int i = 0; i < pair.Value; i++)
                {
                    Add(tourniquetData);
                }
            }
            if (pair.Key == "Keycard LVL 1")
            {
                Add(KeycardDataLvl1);
            }
            if (pair.Key == "Keycard LVL 2")
            {
                Add(KeycardDataLvl2);
            }
            if (pair.Key == "Keycard LVL 3")
            {
                Add(KeycardDataLvl3);
            }
            if (pair.Key == "Keycard LVL 4")
            {
                Add(KeycardDataLvl4);
            }
            if (pair.Key == "Keycard LVL 5")
            {
                Add(KeycardDataLvl5);
            }
        }
    }

    public void SaveData(GameData data)
    {
        Debug.Log("Saving data...");
        foreach (KeyValuePair<string, int> pair in simpleDictionary)
        {
            if (data.simpleDictionary.ContainsKey(pair.Key))
            {
                data.simpleDictionary.Remove(pair.Key);
            }
            data.simpleDictionary.Add(pair.Key, pair.Value);
        }
    }

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
        itemName = itemData.displayName;
        // Check if item exists
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            Debug.Log("Item exists");
            newCount = item.stackSize;
            itemName = item.itemData.displayName;

            item.AddToStack();

            newCount++;

            simpleDictionary[itemName] = newCount;

            Debug.Log($"{item.itemData.displayName} total stack is now {item.stackSize}");
            OnInventoryChange?.Invoke(inventory);
        }
        else
        {
            Debug.Log("Item doesn't exist");
            // create new inventory item
            InventoryItem newItem = new InventoryItem(itemData);
            Debug.Log($"{newItem.itemData.displayName}");
            // store in list
            inventory.Add(newItem);

            // store in dictionary
            itemDictionary.Add(itemData, newItem);

            simpleDictionary.Add(itemName, 1);

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
