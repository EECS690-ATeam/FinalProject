using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tourniquet : MonoBehaviour, ICollectible
{
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
