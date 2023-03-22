using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Vector2[] path = new Vector2[0];

    // I will need to update this to something robust when we introduce more fish,
    // currently this is just for echolocation fish
    [SerializeField] private EcholocationChase chaseObject;

    // Start is called before the first frame update
    void Start()
    {
        /*
        *    Normally, a method must execute to completion within a single frame update.
        *    However, corouttines allow us to continue execution of a method across multiple frames,
        *    this means that a coroutine allows us to continuously move between set points over an
        *    extended period of time without having to continuously check for our current and target
        *    location. While this sounds simillar to threading, this all still happens within the main thread.
        */
        StartCoroutine(StartFollowingPath(path));
    }

    private IEnumerator StartFollowingPath(Vector2[] pathToFollow)
    {
        // Allows unit to patrol endlessly
        while (true)
        {
            // Loop through each point in the path
            foreach (var point in path)
            {
                // Until the unit reaches it's current target position
                while (Vector2.Distance(transform.position, point) > 0.1f)
                {
                    if (!chaseObject.chasing)
                    {

                        // Set rotation to the direction unit is moving
                        transform.right = point - (Vector2)transform.position;

                        // Move towards current target position
                        transform.position = Vector3.MoveTowards(transform.position, point, Time.deltaTime * 10);

                        // Update position each frame, similar to how Update() works
                        yield return new WaitForEndOfFrame();
                    }
                    else
                    {
                        // Do nothing each frame, since chase behavior is controlled by the unit
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
        }
    }
}
