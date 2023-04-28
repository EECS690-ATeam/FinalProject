using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishSpawn : MonoBehaviour
{
    [SerializeField] private Transform movingJellyPrefab;
    [SerializeField] private int numberOfJellies;
    [SerializeField] private float spawnRangeX;
    [SerializeField] private float spawnRangeY;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfJellies; i++)
        {
            Instantiate(movingJellyPrefab, new Vector3(Random.Range(transform.position.x - spawnRangeX, transform.position.x + spawnRangeX), Random.Range(transform.position.y - spawnRangeY, transform.position.y + spawnRangeY), 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
