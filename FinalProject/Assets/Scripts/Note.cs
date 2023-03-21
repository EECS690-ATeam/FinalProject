using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Note : MonoBehaviour, ICollectible
{
    public static event Action OnNoteCollected;

    public void Collect()
    {
        Debug.Log("Note Collected");
        Destroy(gameObject);
        OnNoteCollected?.Invoke();

    }
}
