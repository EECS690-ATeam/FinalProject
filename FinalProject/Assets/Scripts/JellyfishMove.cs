using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float boundsXMin;
    [SerializeField] private float boundsXMax;
    [SerializeField] private float boundsYMin;
    [SerializeField] private float boundsYMax;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5;
        boundsXMin = -105;
        boundsXMax = -40;
        boundsYMin = 29;
        boundsYMax = 60;
    }

    // Update is called once per frame
    void Update()
    {
        var tempPos = transform.position;
        WrapAround(ref tempPos, boundsXMin, boundsXMax, boundsYMin, boundsYMax);
        transform.position = tempPos;
        transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
    }

    void WrapAround(ref Vector3 vector, float minX, float maxX, float minY, float maxY)
    {
        vector.x = WrapAroundFloat(vector.x, minX, maxX);
        vector.y = WrapAroundFloat(vector.y, minY, maxY);
    }

    float WrapAroundFloat(float value, float min, float max)
    {
        if (value > max)
        {
            value = min;
        }
        else if (value < min)
        {
            value = max;
        }
        return value;
    }
}
