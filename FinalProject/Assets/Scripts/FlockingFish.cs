using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingFish : MonoBehaviour
{
    /********************************************************************
    * VARIABLE DECLARATIONS
    ********************************************************************/

    // Instance of a murmurations object, which holds information such as a list of all fish and enemies in the scene
    public Murmurations level;

    // Instance of a configuration object, which contains a set of variables this file uses in order to reduce clutter
    public FlockingFishConfig conf;

    // Current location of this fish
    public Vector3 position;

    // Current velocity of this fish
    public Vector3 velocity;

    // Current acceleration of this fish
    public Vector3 acceleration;

    // Vector which tracks the current location the fish is wandering towards
    Vector3 wanderTarget;

    /********************************************************************
    * METHODS
    ********************************************************************/

    void Start()
    {
        /********************************************************************
        * VARIABLE INITIALIZATIONS
        ********************************************************************/

        level = FindObjectOfType<Murmurations>();
        conf = FindObjectOfType<FlockingFishConfig>();

        // Start all fish with a random velocity
        position = transform.position;
        velocity = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
    }

    void Update()
    {
        // Combine method combines all vector influences into one value which is set as the acceleration
        acceleration = Combine();

        // Acceleration is clamped to whatever value we set as the maximum (prevents snappy movement)
        acceleration = Vector3.ClampMagnitude(acceleration, conf.maxAcceleration);

        // Velocity is set according to current velocity and acceleration, as normal
        velocity = velocity + acceleration * Time.deltaTime;

        // Velocity is clamped to whatever value we set as the maximum (prevents infinite speed)
        velocity = Vector3.ClampMagnitude(velocity, conf.maxVelocity);

        // Position is updated based on current position and velocity, as normal
        position = position + velocity * Time.deltaTime;

        // If fish crosses boundary range, wrap to other side
        WrapAround(ref position, -level.bounds, level.bounds);

        // This is a temporary fix, because z values were being introduced and breaking the distance() function
        position.z = 0;

        // Set current position to the position variable
        transform.position = position;
    }

    // Combines all vector methods to determine final acceleration vector
    virtual protected Vector3 Combine()
    {
        // Each vector method returns a directional vector, which is then multiplied by its priority to determine its influence on the final acceleration vector
        Vector3 finalVec = conf.cohesionPriority * Cohesion() + conf.wanderPriority * Wander()
            + conf.alignmentPriority * Alignment() + conf.separationPriority * Separation()
            + conf.avoidancePriority * Avoidance();

        return finalVec;
    }

    /********************************************************************
    * MOVEMENT VECTOR METHODS
    ********************************************************************/

    // Returns a directional vector pointing towards a random direction
    protected Vector3 Wander()
    {
        // Create vector in a random direction, building off of previous wander targets
        wanderTarget += new Vector3(RandomBinomial(), RandomBinomial(), 0);

        // Convert random vector to a point in world space
        Vector3 targetInWorldSpace = transform.TransformPoint(wanderTarget);

        // Create a vector from the fish to the wander target in world space
        targetInWorldSpace -= this.position;

        // Turn the vector into a directional vector, so it can multiplied later by its priority
        return targetInWorldSpace.normalized;
    }

    // Returns a directional vector pointing towards the average position of neighbors
    Vector3 Cohesion()
    {
        // Vector which will define the average direction of neighbors
        Vector3 cohesionVector = new Vector3();

        // Integer which will hold number of neighbors
        int countMembers = 0;

        // Array containing all neighbors within a given radius
        var neighbors = level.GetNeighbors(this, conf.cohesionRadius);

        // If there are no neighbors within range
        if(neighbors.Count == 0)
        {
            // Return a zero vector
            return cohesionVector;
        }

        // Loop through all neighbors
        foreach(var member in neighbors)
        {
            // If neighbor is in front of you, wihtin your FOV
            if (isInFOV(member.position))
            {
                // Add their position vector to your average
                cohesionVector += member.position;

                // Bump count
                countMembers++;
            }
        }

        // If no neighbors within FOV
        if(countMembers == 0)
        {
            // Return a zero vector
            return cohesionVector;
        }

        cohesionVector /= countMembers;
        cohesionVector = cohesionVector - this.position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;
    }

    // Returns a directional vector pointing towards the average vecloity vector of neighbors
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

    // Returns a directional vector pointing away from the average position of neighbors
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

    // Returns a directional vector pointing away from the average position of nearby enemies
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

    /********************************************************************
    * HELPER METHODS
    ********************************************************************/

    Vector3 RunAway(Vector3 target)
    {
        Vector3 neededVelocity = (position - target).normalized * conf.maxVelocity;
        return neededVelocity - velocity;
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
