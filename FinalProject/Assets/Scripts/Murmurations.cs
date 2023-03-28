using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murmurations : MonoBehaviour
{
    public Transform FlockingFishPrefab;
    public int numberOfFish;
    public List<FlockingFish> flockingFish;
    public List<Avoidance> enemies;
    public float bounds;
    public float spawnRadius;

    // Start is called before the first frame update
    void Start()
    {
        flockingFish = new List<FlockingFish>();
        enemies = new List<Avoidance>();

        Spawn(FlockingFishPrefab, numberOfFish);

        flockingFish.AddRange(FindObjectsOfType<FlockingFish>());
        enemies.AddRange(FindObjectsOfType<Avoidance>());
    }

    void Spawn(Transform prefab, int count)
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(prefab, new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0), Quaternion.identity);
        }
    }

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
