using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murmurations : MonoBehaviour
{
    public Transform flockingFishPrefab;
    public int numberOfFish;
    public List<FlockingFish> flockingFish;

    // Start is called before the first frame update
    void Start()
    {
        flockingFish = new List<FlockingFish>();

        flockingFish.AddRange(FindObjectsOfType<FlockingFish>());
    }

    public List<FlockingFish> GetNeighbors(FlockingFish fish, float radius)
    {
        List<FlockingFish> neighborsFound = new List<FlockingFish>();

        foreach(var otherFish in flockingFish)
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
}
