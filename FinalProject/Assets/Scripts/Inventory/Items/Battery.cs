using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Battery : MonoBehaviour, ICollectible
{
    public static event HandleBatteryCollected OnBatteryCollected;

    // delegate is in place of event to allow return types and args
    public delegate void HandleBatteryCollected(ItemData itemData);

    // reference to scriptable object
    public ItemData batteryData;

    public void Collect()
    {
        Debug.Log("Battery Collected");
        Destroy(gameObject);
        OnBatteryCollected?.Invoke(batteryData);
    }
}
