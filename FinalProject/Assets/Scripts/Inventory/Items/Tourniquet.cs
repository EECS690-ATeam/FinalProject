using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tourniquet : MonoBehaviour, ICollectible, IDataPersistence
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
        Debug.Log("Tourniquet save");
        if (data.itemsCollected.ContainsKey(id))
        {
            data.itemsCollected.Remove(id);
        }
        data.itemsCollected.Add(id, beenCollected);
    }

    public static event HandleTourniquetCollected OnTourniquetCollected;

    // delegate is in place of event to allow return types and args
    public delegate void HandleTourniquetCollected(ItemData itemData);

    // reference to scriptable object
    public ItemData tourniquetData;

    public void Collect()
    {
        Debug.Log("Tourniquet Collected");
        Destroy(gameObject);
        OnTourniquetCollected?.Invoke(tourniquetData);
    }
}
