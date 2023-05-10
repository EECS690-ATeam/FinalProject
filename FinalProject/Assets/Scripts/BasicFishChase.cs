using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFishChase : MonoBehaviour
{
    [SerializeField] private float noticeRange;
    [SerializeField] private float chaseRange;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float returnSpeed;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private AudioSource stinger;

    private Rigidbody2D rb;
    private Transform castPoint;
    private Vector3 post;
    private float currentSpeed;
    private SpriteRenderer sr;

    public bool isChasing;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        isChasing = false;
        post = transform.position;
        castPoint = transform.GetChild(0);
        currentSpeed = 5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 175)
        {
            sr.flipY = true;
        }
        else sr.flipY = false;

        if (CanSeePlayer())
        {
            if (isChasing == false)
            {
                isChasing = true;
                PlayStinger();
            }
            ChasePlayer();
        }
        else
        {
            isChasing = false;
            ReturnToPost();
        }
    }

    private bool CanSeePlayer()
    {
        bool returnVal = false;
        if(PlayerIsInDistance())
        {
            RaycastHit2D hit = Physics2D.Linecast(castPoint.position, playerTransform.position, LayerMask.GetMask("Mask", "Default", "Cover"));
            
            if(hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    returnVal = true;
                }
                Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
            }
            else
            {
                Debug.DrawLine(castPoint.position, playerTransform.position, Color.blue);
            }
        }
        return returnVal;
    }

    private void ChasePlayer()
    {
        Debug.Log("Basic fish chasing player");

        // Play stinger
        SoundManagerScript.PlaySound("stinger");

        // Get angle towards player
        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;
        float playerAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        // Set rotation towards player
        rb.rotation = playerAngle - 180;

        currentSpeed += acceleration * Time.deltaTime;

        // Move directly toward player based on given speed value
        rb.MovePosition((Vector2)transform.position + ((Vector2)playerDirection * Mathf.Clamp(currentSpeed, 0f, maxSpeed) * Time.deltaTime));
    }

    private void ReturnToPost()
    {
        if(Vector3.Distance(transform.position, post) > 0.5f)
        {
            // Play stinger
            SoundManagerScript.PlaySound("stinger");

            // Get angle towards player
            Vector3 playerDirection = (post - transform.position).normalized;
            float playerAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

            // Set rotation towards player
            rb.rotation = playerAngle - 180;

            currentSpeed = 3f;

            // Move directly toward player based on given speed value
            rb.MovePosition((Vector2)transform.position + ((Vector2)playerDirection * returnSpeed * Time.deltaTime));
        }
    }

    /************************
     *  HELPER FUNCTIONS
     ************************/

    private bool PlayerIsInDistance()
    {
        return (Vector3.Distance(castPoint.position, playerTransform.position) < noticeRange);
    }

    void PlayStinger()
    {
        // Play stinger
        stinger.Play();
    }
}
