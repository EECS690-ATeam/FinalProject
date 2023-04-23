using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Player
    public int Health;

    // TODO - Player location
    public Vector2 playerPosition;
   
    // Game
    public string Scene;
    // TODO - List of interaction points visited
    //public List<InteractionPoint> IPsVisited = new List<InteractionPoint>()
    //{

    //}

    // Inventory
    public Dictionary<ItemData, InventoryItem> itemDictionary;


    public GameData()
    {
        this.Health = 100;

        playerPosition = Vector2.zero;
        Scene = "Lab1";

        // Inventory Dictionary
        itemDictionary = new Dictionary<ItemData, InventoryItem>();
    }
}
