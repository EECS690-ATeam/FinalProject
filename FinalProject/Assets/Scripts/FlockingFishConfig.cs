/********************************************************************
* CODE ADAPTED FROM "Renaissance Coders" ON YOUTUBE
* CHANNEL: https://www.youtube.com/@RenaissanceCoders1
* TUTORIAL: https://www.youtube.com/playlist?list=PL4CCSwmU04MhfoJTJWA7n2AIB4dq6umeu
********************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingFishConfig : MonoBehaviour
{
    public float maxFOV = 120;
    public float maxAcceleration;
    public float maxVelocity;

    public float wanderPriority;

    public float cohesionRadius;
    public float cohesionPriority;

    public float alignmentRadius;
    public float alignmentPriority;

    public float separationRadius;
    public float separationPriority;

    public float avoidanceRadius;
    public float avoidancePriority;
}
