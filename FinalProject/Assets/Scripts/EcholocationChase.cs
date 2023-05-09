using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EcholocationChase : MonoBehaviour
{
    [SerializeField] float noticeRange;
    [SerializeField] float chaseRange;
    [SerializeField] float speed;
    [SerializeField] Transform castPoint;
    [SerializeField] Transform player;
    [SerializeField] float patrolClickProbability;
    [SerializeField] float chaseClickProbability;

    private Rigidbody2D rb;
    private Material material;

    public bool chasing;
    public Transform EchoPingPrefab;
    public Transform PulsePrefab;
    public Animator anim;

    public Transform GameObject;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chasing = false;
        material = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameObject.eulerAngles.z>0 && GameObject.eulerAngles.z < 160) {
            sr.flipY = true;
        }
        else sr.flipY = false;


        // Chase player only if within range
        if (CanSeePlayer(noticeRange))
        {
            chasing = true;
            ChasePlayer();
            float randomNum = Random.Range(0f, 100f);
            if(randomNum < chaseClickProbability)
            {
                SoundManagerScript.PlaySound("click_2_");
                Instantiate(EchoPingPrefab, transform.position + new Vector3(-0.3f, 0.2f), Quaternion.identity);
                Instantiate(PulsePrefab, transform.position, transform.rotation);
            }
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).name == "BaseSprite")
                {
                    material = gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().material;
                }
            }
            if(material != null)
            {
                material.SetColor("_Color", new Color(36f, 0f, 0f, 1f));
            }
        }
        else if (chasing && CanStillSeePlayer(chaseRange))
        {
            ChasePlayer();
            float randomNum = Random.Range(0f, 100f);
            if (randomNum < chaseClickProbability)
            {
                SoundManagerScript.PlaySound("click_2_");
                Instantiate(EchoPingPrefab, transform.position + new Vector3(-0.3f, 0.2f), Quaternion.identity);
                Instantiate(PulsePrefab, transform.position, transform.rotation);
            }
        }
        else
        {
            chasing = false;
            StopChasingPlayer();
            float randomNum = Random.Range(0f, 100f);
            if (randomNum < patrolClickProbability)
            {
                SoundManagerScript.PlaySound("click_2_");
                Instantiate(EchoPingPrefab, transform.position + new Vector3(-0.3f, 0.2f), Quaternion.identity);
                Instantiate(PulsePrefab, transform.position, transform.rotation);
            }
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).name == "BaseSprite")
                {
                    material = gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().material;
                }
            }
            if(material != null)
            {
                material.SetColor("_Color", new Color(0.5f, 0f, 0f, 0f));
            }
        }
    }

    bool CanSeePlayer(float searchRange)
    {
        // Initialize return value
        bool returnVal = false;

        // Get point for end of raycast, depending on current facing direction
        Vector2 endOfCast = castPoint.position + (transform.up * searchRange);

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

        // Color of raycast
        Color castColor = Color.blue;

        // If raycast hit something
        if (hit.collider != null)
        {
            // If raycast hit player
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                returnVal = true;
                castColor = Color.red;
            }
            else
            {
                castColor = Color.yellow;
            }
        }

        // Draw line to visualize cast
        Debug.DrawLine(castPoint.position, hit.point, castColor);

        return returnVal;
    }

    void ChasePlayer()
    {
        anim.SetBool("IsChase", true);

        // Play stinger
        SoundManagerScript.PlaySound("stinger");

        // Get angle towards player
        Vector3 playerDirection = (player.position - transform.position).normalized;
        float playerAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        // Set rotation towards player
        rb.rotation = playerAngle - 90;

        // Move directly toward player based on given speed value
        rb.MovePosition((Vector2)transform.position + ((Vector2)playerDirection * speed * Time.deltaTime));
    }

    void StopChasingPlayer()
    {
        anim.SetBool("IsChase", false);
        rb.velocity = new Vector2(0, 0);
    }
}
