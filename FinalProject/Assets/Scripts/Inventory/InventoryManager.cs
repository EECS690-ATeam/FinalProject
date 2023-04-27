using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>(24);
    public InventoryMenu inventoryMenu;

    //Register to OnInventoryChange Event
    private void OnEnable()
    {
        Debug.Log("Inventory Manager Enable");
        Inventory.OnInventoryChange += DrawInventory;
    }

    private void OnDisable()
    {
        Debug.Log("Inventory Manager Disable");
        Inventory.OnInventoryChange -= DrawInventory;
    }

   
    // DISADVANTAGE TO THIS APPROACH
    // When adding a new item or even incrementing the count of an existing item,
    // we have to delete entire inventory and recreate it. Can be very slow with large inventory systems
    // I believe this won't affect our game too much as it's not heavily focused on inventory 
    void ResetInventory()
    {
        Debug.Log("Reset Inventory");
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        inventorySlots = new List<InventorySlot>(24);
    }

    void DrawInventory(List<InventoryItem> inventory)
    {
        Debug.Log("Drawing Inventory");
        ResetInventory();
        // create new empty inventory
        for (int i = 0; i < inventorySlots.Capacity; i++)
        {
            CreateInventorySlot();
        }
        // iterate through old inventory and update new inventory accordingly
        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySlots[i].DrawSlot(inventory[i]);
        }
        if (!inventoryMenu.isOpen)
        {
            for (int i = 0; i < inventoryMenu.inventoryGrid.transform.childCount; i++)
            {
                inventoryMenu.inventoryGrid.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                inventoryMenu.inventoryGrid.transform.GetChild(i).transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 0f);
                inventoryMenu.inventoryGrid.transform.GetChild(i).transform.GetChild(2).GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }

    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        // make sure new game object is nested under InventoryPanel as a child
        newSlot.transform.SetParent(transform, false);

        // get inventory slot component off of new prefab
        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();

        inventorySlots.Add(newSlotComponent);
    }
}
