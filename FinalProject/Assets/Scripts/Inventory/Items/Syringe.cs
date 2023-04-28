using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Syringe : MonoBehaviour, ICollectible, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Create guid for id")]

    private void GenerateGiud()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public static event HandleSyringeCollected OnSyringeCollected;

    // delegate is in place of event to allow return types and args
    public delegate void HandleSyringeCollected(ItemData itemData);

    // reference to scriptable object
    public ItemData syringeData;

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
        if (data.itemsCollected.ContainsKey(id))
        {
            data.itemsCollected.Remove(id);
        }
        data.itemsCollected.Add(id, beenCollected);
    }

    public void Collect()
    {
        Debug.Log("Syringe Collected");
        gameObject.SetActive(false);
        beenCollected = true;
        OnSyringeCollected?.Invoke(syringeData);
    }
}
