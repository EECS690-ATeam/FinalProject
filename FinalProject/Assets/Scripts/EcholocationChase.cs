using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

/*
 * Planned behavior for echolocation fish:
 * 
 * Fish swims in predetermined path around the map searching for pray
 * While in this state it casts a ray directly infront of itself
 * If player intersects the ray, a new, longer ray is casted which tracks the player
 * In order to get away, the player must either get out of range of the new ray,
 * or hide behind some obstacle which breaks the fishes line of sight
 * The first option is unlikely as the echolocation fish should be faster than the player
 * Also, need to create logic so that the fish returns to its patrol path after a chase     
 * 
 * Current behavior:
 * 
 * The echolocation fish always faces and tracks the player, though only chases once in a certain range
 * Chase is ended when line of sight is broken
 * 
 * Stretch goals:
 * 
 * Would like for the echolocation fish to utilize "right" and "left" raycasts so that it can
 * navigate around obstacles in trivial cases. Sort of how trivial "AI driven" RC cars work
 */
public class EcholocationChase : MonoBehaviour
{
    [SerializeField] float noticeRange;
    [SerializeField] float chaseRange;
    [SerializeField] float speed;
    [SerializeField] Transform castPoint;
    [SerializeField] Transform player;

    Rigidbody2D rb;
    bool chasing;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chasing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Chase player only if within range
        if (CanSeePlayer(noticeRange))
        {
            chasing = true;
            ChasePlayer();
        }
        else if (chasing && CanStillSeePlayer(chaseRange))
        {
            ChasePlayer();
        }
        else
        {
            chasing = false;
            StopChasingPlayer();
        }
    }

    bool CanSeePlayer(float searchRange)
    {
        // Initialize return value
        bool returnVal = false;

        // Get point for end of raycast, depending on current facing direction
        Vector2 endOfCast = castPoint.position + (transform.right * searchRange);

        // Create raycast towards facing direction
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endOfCast, LayerMask.NameToLayer("Action"));

        // If raycast hit something
        if (hit.collider != null)
        {
            // If raycast hit player
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                returnVal = true;
            }
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endOfCast, Color.blue);
        }

        return returnVal;
    }

    bool CanStillSeePlayer(float searchRange)
    {
        // Initialize return value
        bool returnVal = false;

        // Get angle towards player
        Vector3 playerDirection = (player.position - castPoint.position).normalized;
        float rayCastAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        // Get point for end of raycast, depending on search range
        Vector2 endOfCast = castPoint.position + UtilsClass.GetVectorFromAngle(rayCastAngle) * searchRange;

        // Create raycast towards player
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endOfCast, LayerMask.NameToLayer("Action"));

        // If raycast hit something
        if (hit.collider != null)
        {
            // If raycast hit player
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                returnVal = true;
            }
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endOfCast, Color.blue);
        }

        return returnVal;
    }

    void ChasePlayer()
    {
        // Get angle towards player
        Vector3 playerDirection = (player.position - transform.position).normalized;
        float playerAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        // Set rotation towards player
        rb.rotation = playerAngle;

        // Move directly toward player based on given speed value
        rb.MovePosition((Vector2)transform.position + ((Vector2)playerDirection * speed * Time.deltaTime));
    }

    void StopChasingPlayer()
    {
        rb.velocity = new Vector2(0, 0);
    }
}
