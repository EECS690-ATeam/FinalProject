using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Syringe : MonoBehaviour, ICollectible
{
    // static event that things can subscribe to
    // good idea to break every item into its own event

    public static event HandleSyringeCollected OnSyringeCollected;
    // delegate is in place of event to allow return types and args
    public delegate void HandleSyringeCollected(ItemData itemData);

    // reference to scriptable object
    public ItemData syringeData;

    public void Collect()
    {
        Debug.Log("Syringe Collected");
        Destroy(gameObject);
        OnSyringeCollected?.Invoke(syringeData);
    }
}
