using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Determine if we collided with something "collectible" and tell it to collect itself
public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        // If exists, collect
        if(collectible != null)
        {
            collectible.Collect();
        }
    }
}
