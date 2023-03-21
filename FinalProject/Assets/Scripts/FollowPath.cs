using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Vector2[] path = new Vector2[0];

    // Start is called before the first frame update
    void Start()
    {
        /*
        *    Normally, a method must execute to completion within a single frame update
        *    However, corouttines allow you to continue execution of a method across multiple frames,
        *    This means that a coroutine should not delay frame updates. While this sounds simillar to
        *    threading, this all still happens within the main thread.
        *    Ultimately this is kind of pointless for out project, I think it exists just in the case that
        *    you have dozens or more units patrolling simultaneously; however, it also isn't huring anything.
        *    Should be able to change this pretty painlessly if we like
        *    
        *    Also could prove useful if the case that we create parahna fish and do have dozens of units patrolling
        */
        StartCoroutine(StartFollowingPath(path));
    }

    private IEnumerator StartFollowingPath(Vector2[] pathToFollow)
    {
        // Allows the unit to patrol endlessly
        while (true)
        {
            // Loop through each point in the path
            foreach (var point in pathToFollow)
            {
                // Until the unit reaches it's current target position
                while (Vector2.Distance(transform.position, point) > 0.1f)
                {
                    // Move towards current target position
                    transform.position = Vector3.MoveTowards(transform.position, point, Time.deltaTime * 10);

                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
}
