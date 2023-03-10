using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCamera : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.1f;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if(player.position.x < 27.28 && player.position.x > -20.69) 
            {
                Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            }
        }
    }
}


