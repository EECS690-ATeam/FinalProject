using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Battery : MonoBehaviour, ICollectible, IDataPersistence
{

    [SerializeField] private string id;
    [ContextMenu("Create guid for id")]

    private void GenerateGiud()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public bool beenCollected = false;

    public void LoadData(GameData data)
    {
        data.itemsCollected.TryGetValue(id, out beenCollected);
        if (beenCollected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        Debug.Log("Battery save");
        if (data.itemsCollected.ContainsKey(id))
        {
            data.itemsCollected.Remove(id);
        }
        data.itemsCollected.Add(id, beenCollected);
    }

    public static event HandleBatteryCollected OnBatteryCollected;

    // delegate is in place of event to allow return types and args
    public delegate void HandleBatteryCollected(ItemData itemData);

    // reference to scriptable object
    public ItemData batteryData;

    public void Collect()
    {
        Debug.Log("Battery Collected");
        //Destroy(gameObject);
        gameObject.SetActive(false);
        beenCollected = true;
        OnBatteryCollected?.Invoke(batteryData);
    }
}
