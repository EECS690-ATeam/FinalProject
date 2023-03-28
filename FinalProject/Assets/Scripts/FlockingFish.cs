using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingFish : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    public Murmurations level;
    public FlockingFishConfig conf;

    Vector3 wanderTarget;

    void Start()
    {
        level = FindObjectOfType<Murmurations>();
        conf = FindObjectOfType<FlockingFishConfig>();

        position = transform.position;
        velocity = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
    }

    void Update()
    {
        acceleration = Combine();
        acceleration = Vector3.ClampMagnitude(acceleration, conf.maxAcceleration);
        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, conf.maxVelocity);
        position = position + velocity * Time.deltaTime;
        WrapAround(ref position, -level.bounds, level.bounds);
        position.z = 0;
        transform.position = position;
    }

    protected Vector3 Wander()
    {
        // Distance to move in a frame
        float jitter = conf.wanderJitter * Time.deltaTime;

        wanderTarget += new Vector3(RandomBinomial() * jitter, RandomBinomial() * jitter, 0);
        wanderTarget = wanderTarget.normalized;
        wanderTarget *= conf.wanderRadius;
        Vector3 targetInLocalSpace = wanderTarget + new Vector3(conf.wanderDistance, conf.wanderDistance, 0);
        Vector3 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);
        targetInWorldSpace -= this.position;
        return targetInWorldSpace.normalized;
    }

    Vector3 Cohesion()
    {
        Vector3 cohesionVector = new Vector3();
        int countMembers = 0;
        var neighbors = level.GetNeighbors(this, conf.cohesionRadius);
        if(neighbors.Count == 0)
        {
            return cohesionVector;
        }
        foreach(var member in neighbors)
        {
            if (isInFOV(member.position))
            {
                cohesionVector += member.position;
                countMembers++;
            }
        }

        if(countMembers == 0)
        {
            return cohesionVector;
        }

        cohesionVector /= countMembers;
        cohesionVector = cohesionVector - this.position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;
    }

    Vector3 Alignment()
    {
        Vector3 alignVector = new Vector3();
        var members = level.GetNeighbors(this, conf.alignmentRadius);
        if(members.Count == 0)
        {
            return alignVector;
        }

        foreach(var member in members)
        {
            if (isInFOV(member.position))
            {
                alignVector += member.velocity;
            }
        }

        return alignVector.normalized;
    }

    Vector3 Separation()
    {
        Vector3 seperateVector = new Vector3();
        var members = level.GetNeighbors(this, conf.separationRadius);
        if (members.Count == 0)
        {
            return seperateVector;
        }

        foreach(var member in members)
        {
            if(isInFOV(member.position))
            {
                Vector3 movingTowards = this.position - member.position;
                if(movingTowards.magnitude > 0)
                {
                    seperateVector += movingTowards.normalized / movingTowards.magnitude;
                }
            }
        }

        return seperateVector.normalized;
    }

    Vector3 Avoidance()
    {
        Vector3 avoidVector = new Vector3();
        var enemyList = level.GetEnemies(this, conf.avoidanceRadius);
        if (enemyList.Count == 0)
        {
            return avoidVector;
        }
        foreach (var enemy in enemyList)
        {
            avoidVector += RunAway(enemy.position);
        }

        return avoidVector.normalized;
    }

    Vector3 RunAway(Vector3 target)
    {
        Vector3 neededVelocity = (position - target).normalized * conf.maxVelocity;
        return neededVelocity - velocity;
    }

    virtual protected Vector3 Combine()
    {
        /*
        Debug.Log("Cohesion Prio: " + conf.cohesionPriority);
        Debug.Log("Wander Prio: " + conf.wanderPriority);
        Debug.Log("Alignment Prio: " + conf.alignmentPriority);
        Debug.Log("Separation Prio: " + conf.separationPriority);
        Debug.Log("Avoidance Prio: " + conf.avoidancePriority);

        Debug.Log("Cohesion: " + Cohesion());
        Debug.Log("Wander: " + Wander());
        Debug.Log("Alignment: " + Alignment());
        Debug.Log("Separation: " + Separation());
        Debug.Log("Avoidance: " + Avoidance());

        Debug.Log("Everything Else: " + (conf.cohesionPriority * Cohesion().magnitude + conf.wanderPriority * Wander().magnitude + conf.alignmentPriority * Alignment().magnitude + conf.separationPriority * Separation().magnitude)
            + " Avoidance: " + conf.avoidancePriority * Avoidance().magnitude);
        */

        Vector3 finalVec = conf.cohesionPriority * Cohesion() + conf.wanderPriority * Wander()
            + conf.alignmentPriority * Alignment() + conf.separationPriority * Separation()
            + conf.avoidancePriority * Avoidance();
        return finalVec;
    }

    void WrapAround(ref Vector3 vector, float min, float max)
    {
        vector.x = WrapAroundFloat(vector.x, min, max);
        vector.y = WrapAroundFloat(vector.y, min, max);
        vector.z = WrapAroundFloat(vector.z, min, max);
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

    float RandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }

    bool isInFOV(Vector3 vec)
    {
        return Vector3.Angle(this.velocity, vec - this.position) <= conf.maxFOV;
    }
}
