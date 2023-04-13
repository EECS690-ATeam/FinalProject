using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    bool facingRight=true;

    public static Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        this.transform.position += spawnPos;
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if(moveInput<0 && facingRight) {
            facingRight = !facingRight;
            sr.flipX = false;
        }
        if(moveInput>0 && !facingRight) {
            facingRight = !facingRight;
            sr.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (collision.gameObject.name == "RightBorder" && (Input.GetAxisRaw("Horizontal") > 0))
        {
            if (sceneName == "Lab1") {
                PlayerMovement.spawnPos = new Vector3(-26, 0, 0);
                SceneManager.LoadScene("Kelp Cavern");
            }

            else if (sceneName == "Lab2") {
                PlayerMovement.labSpawn = 2;
                SceneManager.LoadScene("Exterior Area");
            }

            else if (sceneName == "Lab3") {
                PlayerMovement.labSpawn = 2;
                SceneManager.LoadScene("Exterior Area");
            }
        }
        if (collision.gameObject.name == ("LeftBorder") && (Input.GetAxisRaw("Horizontal") < 0))
        {
            if (sceneName == "Lab1") {
                PlayerMovement.labSpawn = 1;
                SceneManager.LoadScene("Exterior Area");
            }

            else if (sceneName == "Lab2") {
                PlayerMovement.spawnPos = new Vector3(64, 0, 0);
                SceneManager.LoadScene("Kelp Cavern");
            }

            else if (sceneName == "Lab3") {
                PlayerMovement.spawnPos = new Vector3(64, 0, 0);
                SceneManager.LoadScene("Dark Cavern");
            }
        }
    }
}
