using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// can inherit from a bunch of interfaces but only one class
public class Keycard : MonoBehaviour, ICollectible, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Create guid for id")]

    private void GenerateGiud()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public static event HandleKeycardCollected OnKeycardCollected;

    // delegate is in place of event to allow return types and args
    public delegate void HandleKeycardCollected(ItemData itemData);

    // reference to scriptable object
    public ItemData keycardData;

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
        Debug.Log("Keycard save");
        if (data.itemsCollected.ContainsKey(id))
        {
            data.itemsCollected.Remove(id);
        }
        data.itemsCollected.Add(id, beenCollected);
    }

    public void Collect()
    {
        Debug.Log("Keycard Collected");
        gameObject.SetActive(false);
        beenCollected = true;
        OnKeycardCollected?.Invoke(keycardData);
    }
}
