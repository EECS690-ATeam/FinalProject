using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenu : MonoBehaviour
{
    public GameObject inventoryMenu;
    public GameObject inventoryGrid;
    // Use global bool to check if game is paused across all scenes
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;

        inventoryMenu.SetActive(true);
        inventoryGrid.SetActive(true);

        inventoryMenu.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        for(int i = 0; i < inventoryGrid.transform.childCount; i++)
        {
            inventoryGrid.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            inventoryGrid.transform.GetChild(i).transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 0f);
            inventoryGrid.transform.GetChild(i).transform.GetChild(2).GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
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
        inventoryMenu.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        for (int i = 0; i < inventoryGrid.transform.childCount; i++)
        {
            inventoryGrid.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            inventoryGrid.transform.GetChild(i).transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 1f);
            inventoryGrid.transform.GetChild(i).transform.GetChild(2).GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 1f);
        }
        isOpen = true;
    }

    public void CloseInventory()
    {
        inventoryMenu.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        for (int i = 0; i < inventoryGrid.transform.childCount; i++)
        {
            inventoryGrid.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            inventoryGrid.transform.GetChild(i).transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 0f);
            inventoryGrid.transform.GetChild(i).transform.GetChild(2).GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 0f);
        }
        isOpen = false;
    }
}
