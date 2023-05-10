/********************************************************************
* CODE ADAPTED FROM "Renaissance Coders" ON YOUTUBE
* CHANNEL: https://www.youtube.com/@RenaissanceCoders1
* TUTORIAL: https://www.youtube.com/playlist?list=PL4CCSwmU04MhfoJTJWA7n2AIB4dq6umeu
********************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murmurations : MonoBehaviour
{
    /********************************************************************
    * VARIABLE DECLARATIONS
    ********************************************************************/
    public Transform FlockingFishPrefab;
    public int numberOfFish;
    public List<FlockingFish> flockingFish;
    public List<Avoidance> enemies;
    public float minBoundsX;
    public float maxBoundsX;
    public float minBoundsY;
    public float maxBoundsY;
    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;

    // Start is called before the first frame update
    void Start()
    {
        /********************************************************************
        * VARIABLE INITIALIZATIONS
        ********************************************************************/
        flockingFish = new List<FlockingFish>();
        enemies = new List<Avoidance>();

        /********************************************************************
        * INSTANTIATING FISH
        ********************************************************************/
        Spawn(FlockingFishPrefab, numberOfFish);

        flockingFish.AddRange(FindObjectsOfType<FlockingFish>());
        enemies.AddRange(FindObjectsOfType<Avoidance>());
    }

    // Method that instantiates several flocking fish, depending on the given count
    void Spawn(Transform prefab, int count)
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(prefab, new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY), 0), Quaternion.identity);
        }
    }

    // Method that Returns a List of neighbors of a given fish, based on a given radius
    public List<FlockingFish> GetNeighbors(FlockingFish fish, float radius)
    {
        List<FlockingFish> neighborsFound = new List<FlockingFish>();

        foreach (var otherFish in flockingFish)
        {
            if(otherFish == fish)
            {
                continue;
            }
            if(Vector3.Distance(fish.position, otherFish.position) <= radius)
            {
                neighborsFound.Add(otherFish);
            }
        }

        return neighborsFound;
    }

    // Method that Returns a List of nearby enemies of a given fish, based on a given radius
    public List<Avoidance> GetEnemies(FlockingFish fish, float radius)
    {
        List<Avoidance> returnEnemies = new List<Avoidance>();
        foreach(var enemy in enemies)
        {
            if (Vector3.Distance(fish.position, enemy.position) <= radius)
            {
                returnEnemies.Add(enemy);
            }
        }
        return returnEnemies;
    }
}
