using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torniquet : MonoBehaviour, ICollectible
{
    public static event HandleTorniquetCollected OnTorniquetCollected;
    // delegate is in place of event to allow return types and args
    public delegate void HandleTorniquetCollected(ItemData itemData);
    // reference to scriptable object
    public ItemData torniquetData;
    public void Collect()
    {
        Debug.Log("Torniquet Collected");
        Destroy(gameObject);
        OnTorniquetCollected?.Invoke(torniquetData);

    }
}
