using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Health;
    public Vector3 playerPosition;
   
    // Game
    public string scene;
    // TODO - List of interaction points visited
    public SerializableDictionary<string, bool> ipsVisited;

    // Inventory
    public SerializableDictionary<string, int> simpleDictionary;

    public SerializableDictionary<string, bool> itemsCollected;


    public GameData()
    {
        this.Health = 100;
        // TODO - should initialize to starting position in lab1
        playerPosition = new Vector3(8.08f, -1.07f, -0.10f);

        scene = "Start Menu";

        ipsVisited = new SerializableDictionary<string, bool>();

        // Inventory Dictionary
        simpleDictionary = new SerializableDictionary<string, int>();

        itemsCollected = new SerializableDictionary<string, bool>();

    }
}
