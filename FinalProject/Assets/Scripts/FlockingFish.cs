/********************************************************************
* CODE ADAPTED FROM "Renaissance Coders" ON YOUTUBE
* CHANNEL: https://www.youtube.com/@RenaissanceCoders1
* TUTORIAL: https://www.youtube.com/playlist?list=PL4CCSwmU04MhfoJTJWA7n2AIB4dq6umeu
********************************************************************/

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
                // Add their position vector, this will create a very large vector which when
                // divided by the count of neighbors will describe their average position
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

        // Divide massive vector by member count to get average position
        cohesionVector /= countMembers;

        // Subtract your position from average neighbor position to get a vector from your location to average position
        cohesionVector = cohesionVector - this.position;

        // Convert vector into directional vector
        cohesionVector = Vector3.Normalize(cohesionVector);

        // Return vector to later be multiplied by priority
        return cohesionVector;
    }

    // Returns a directional vector pointing towards the average vecloity vector of neighbors
    Vector3 Alignment()
    {
        // Create vector that will hold the average velocity of neighbors
        Vector3 alignVector = new Vector3();

        // Array of all neighbors within a given range
        var members = level.GetNeighbors(this, conf.alignmentRadius);

        // If no neighbors within given range
        if(members.Count == 0)
        {
            // Return a zero vector
            return alignVector;
        }

        // Loop through all neighbors in range
        foreach(var member in members)
        {
            // If neigbors is within defined FOV in front of you
            if (isInFOV(member.position))
            {
                // Add velocity vector, this will create a massive vector pointing in the average direction of all neighbors
                alignVector += member.velocity;
            }
        }

        // Convert vector into a directional vector, which will be multiplied by priority
        return alignVector.normalized;
    }

    // Returns a directional vector pointing away from the average position of neighbors
    Vector3 Separation()
    {
        // Vector which will point away from the average position of neighbors within a given range
        Vector3 seperateVector = new Vector3();

        // Array of all neighbors wihtin a given range
        var members = level.GetNeighbors(this, conf.separationRadius);

        // If no neighbors within the given range
        if (members.Count == 0)
        {
            // Return a zero vector
            return seperateVector;
        }

        // Loop through all neighbors within the given range
        foreach(var member in members)
        {
            // If neighbor is within a given FOV in front of you
            if(isInFOV(member.position))
            {
                // Add vector away from neighbor, this will create a very large vector in the direction away from the average neighbor
                seperateVector += this.position - member.position;
            }
        }

        // Convert vector to a directional vector, to be later multiplied by priority
        return seperateVector.normalized;
    }

    // Returns a directional vector pointing away from the average position of nearby enemies
    Vector3 Avoidance()
    {
        // Vector to hold direction away from average enemy within range
        Vector3 avoidVector = new Vector3();

        // Array of all enemies within a given range
        var enemyList = level.GetEnemies(this, conf.avoidanceRadius);

        // If no enemies within a given range
        if (enemyList.Count == 0)
        {
            // Return a zero vector
            return avoidVector;
        }

        // Loop through all enemies within range
        foreach (var enemy in enemyList)
        {
            // Add vector
            avoidVector += this.position - enemy.position;
        }

        return avoidVector.normalized;
    }

    /********************************************************************
    * HELPER METHODS
    ********************************************************************/

    // Takes a vector by reference and wraps its x and y values if needed
    void WrapAround(ref Vector3 vector, float min, float max)
    {
        vector.x = WrapAroundFloat(vector.x, min, max);
        vector.y = WrapAroundFloat(vector.y, min, max);
    }

    // Takes a value and returns a wrapped value if it is too low or high
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

    // Returns a random float from -1 to 1
    float RandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }

    // Checks whether the angle of a target position is within a given range
    bool isInFOV(Vector3 vec)
    {
        return Vector3.Angle(this.velocity, vec - this.position) <= conf.maxFOV;
    }
}
