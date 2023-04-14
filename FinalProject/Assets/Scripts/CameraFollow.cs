using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform border;
    public float smoothTime = 0.1f;
    public float swayAmplitude = 1f;
    public float swayFrequency = 0.5f;

    private Vector3 velocity = Vector3.zero;
    private float currentSway;
    private Vector3 swayVector;

    private float leftLimit;
    private float rightLimit;
    private float topLimit;
    private float bottomLimit;

    // Start is called before the first frame update
    void Start()
    {
        //calculating camera bounds
        SpriteRenderer borderRenderer = border.GetComponent<SpriteRenderer>();
        leftLimit = border.position.x - borderRenderer.bounds.extents.x;
        rightLimit = border.position.x + borderRenderer.bounds.extents.x;
        topLimit = border.position.y + borderRenderer.bounds.extents.y;
        bottomLimit = border.position.y - borderRenderer.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {

            currentSway = (float)Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;

            Vector3 targetPosition = new Vector3(player.position.x + currentSway, player.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, leftLimit, rightLimit);
            targetPosition.y = Mathf.Clamp(targetPosition.y, bottomLimit, topLimit);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}