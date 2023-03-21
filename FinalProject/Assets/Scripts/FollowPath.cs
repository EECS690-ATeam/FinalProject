using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Vector2[] path = new Vector2[0];

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFollowingPath(path));
    }

    private IEnumerator StartFollowingPath(Vector2[] pathToFollow)
    {
        foreach(var point in pathToFollow)
        {
            while(Vector2.Distance(transform.position, point) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, point, Time.deltaTime * 10);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
