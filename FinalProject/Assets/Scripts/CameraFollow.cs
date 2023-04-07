using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.1f;
    public float swayAmplitude = 1f;
    public float swayFrequency = 0.5f;

    private Vector3 velocity = Vector3.zero;
    private float currentSway;
    private Vector3 swayVector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            currentSway = (float)Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;

            Debug.Log(currentSway);

            Vector3 targetPosition = new Vector3(player.position.x + currentSway, player.position.y, transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}