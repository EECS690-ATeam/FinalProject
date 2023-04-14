using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    //public GameObject inventoryMenu;
    public GameObject inventoryGrid;
    // Use global bool to check if game is paused across all scenes
    public static bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        //inventoryMenu.SetActive(false);
        inventoryGrid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    public void OpenInventory()
    {
        //inventoryMenu.SetActive(true);
        inventoryGrid.SetActive(true);
        isOpen = true;
    }

    public void CloseInventory()
    {
        //inventoryMenu.SetActive(false);
        inventoryGrid.SetActive(false);
        isOpen = false;
    }
}
