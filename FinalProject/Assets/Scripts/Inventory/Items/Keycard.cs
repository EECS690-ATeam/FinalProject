using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// can inherit from a bunch of interfaces but only one class
public class Keycard : MonoBehaviour, ICollectible
{
    public static event HandleKeycardCollected OnKeycardCollected;

    // delegate is in place of event to allow return types and args
    public delegate void HandleKeycardCollected(ItemData itemData);

    // reference to scriptable object
    public ItemData keycardData;

    public void Collect()
    {
        Debug.Log("Keycard Collected");
        Destroy(gameObject);
        OnKeycardCollected?.Invoke(keycardData);
    }
}
