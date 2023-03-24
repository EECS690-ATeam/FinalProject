using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 150f;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    Vector2 movement;
    LeftBorder lBorder;
    RightBorder rBorder;
    

    void Start()
    {
        lBorder = FindObjectOfType<LeftBorder>();
        rBorder = FindObjectOfType<RightBorder>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        movement = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    //// Not tied to the frame rate like Update() is
    //void FixedUpdate()
    //{
    //    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (collision.gameObject.name == "RightBorder" && (Input.GetAxisRaw("Horizontal") > 0))
        {
            if (sceneName == "Exterior Area") {
                float newX = lBorder.transform.position[0] + lBorder.GetComponent<SpriteRenderer>().bounds.size.x/2 - playerCollider.bounds.size.x/2;
                Vector3 temp = new Vector3(this.transform.position.x - newX, 0f, 0f);
                this.transform.position -= temp;
            }

            else if (sceneName == "Kelp Cavern") {
                SceneManager.LoadScene("Lab2");
            }
        }
        if (collision.gameObject.name == ("LeftBorder") && (Input.GetAxisRaw("Horizontal") < 0))
        {
            if (sceneName == "Exterior Area") {
                float newX = rBorder.transform.position[0] - rBorder.GetComponent<SpriteRenderer>().bounds.size.x/2 + playerCollider.bounds.size.x/2;
                Vector3 temp = new Vector3(newX - this.transform.position.x, 0f, 0f);
                this.transform.position += temp;
            }

            else if (sceneName == "Kelp Cavern") {
                SceneManager.LoadScene("Lab1");
            }
        }
        //exterior area lab entrance colliders
        if (collision.gameObject.name == "Lab1")
        {
            SceneManager.LoadScene("Lab1");
        }
        if (collision.gameObject.name == "Lab2")
        {
            SceneManager.LoadScene("Lab2");
        }
    }
}