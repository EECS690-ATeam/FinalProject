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

    public Transform GameObject;
    public Animator animator;
    public float MovementSpeed = 1;
    bool facingRight = true;
    private SpriteRenderer sr;
    public float verticalRot = 30;
    public float horizontalRot = 180;
    bool inc = false;
    bool dec = false;

    bool orientation = false; //false = vertical -> true = horizontal
    
    //controlled for each scene transition
    public static Vector3 spawnPos;
    public static int labSpawn;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator.SetBool("IsIdle", true);
        lBorder = FindObjectOfType<LeftBorder>();
        rBorder = FindObjectOfType<RightBorder>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        movement = rb.velocity;
        if (labSpawn != 0) {
            spawnAtLab();
        }
        else {
            this.transform.position += spawnPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // // Input
        // movement.x = Input.GetAxisRaw("Horizontal");
        // movement.y = Input.GetAxisRaw("Vertical");


        // rb.velocity = new Vector2(movement.x, movement.y).normalized * moveSpeed;
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");
        if(horizontalMovement != 0) animator.SetBool("IsIdle", false);
        else animator.SetBool("IsIdle", true);
        transform.position += new Vector3(horizontalMovement, verticalMovement, 0).normalized * Time.deltaTime * MovementSpeed;


        if(!orientation && horizontalMovement!=0) { 
            if((GameObject.eulerAngles.z>270 && GameObject.eulerAngles.z<=360)) {
                if(facingRight) transform.Rotate(new Vector3(0,0,-horizontalMovement)); //Right angle
                else transform.Rotate(new Vector3(0,0,-1));
            }
            if(GameObject.eulerAngles.z<270 && GameObject.eulerAngles.z > 90) {
                orientation = true;
            }
        }
        else if(horizontalMovement==0) {
            if(GameObject.eulerAngles.z>220 && GameObject.eulerAngles.z<359) {
                transform.Rotate(new Vector3(0,0,.45f));
            }
        }

        if(orientation) {
            if(GameObject.eulerAngles.z>225 && GameObject.eulerAngles.z<320) {
                transform.Rotate(new Vector3(0,0,verticalMovement));
            }
            if(GameObject.eulerAngles.z<225) inc = true;
            if(GameObject.eulerAngles.z>320) {
                if(horizontalMovement==0) orientation = false;
                if(horizontalMovement==0) animator.SetBool("IsIdle", true);
            }
        }

        if(inc) {
            transform.Rotate(new Vector3(0,0,2));
            inc = false;
        }
        if(dec) {
            transform.Rotate(new Vector3(0,0,2));
            dec = false;
        }

        //Y Axis 180 Rotation when switching sides
        if(transform.rotation.y >0 && transform.rotation.y < 180 && horizontalMovement < 0) {
            transform.Rotate(new Vector3(0,Time.deltaTime,0) * horizontalRot*2, Space.World);
        }
        if(horizontalMovement<0 && facingRight) {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0,Time.deltaTime,0) * horizontalRot);
        }
        if(transform.rotation.y > -180 && transform.rotation.y < 0 && horizontalMovement >0) {
            transform.Rotate(new Vector3(0,Time.deltaTime,0)*horizontalRot*2, Space.World);
        }
        if(horizontalMovement>0 && !facingRight) {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0,Time.deltaTime,0) * horizontalRot);
        }
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
        if(collision.gameObject.name == "EcholocationFish")
        {
            spawnPos = new Vector3(-26, 0, 0);
            SceneManager.LoadScene("Kelp Cavern");
        }
        if (collision.gameObject.name == "RightBorder" && (Input.GetAxisRaw("Horizontal") > 0))
        {
            if (sceneName == "Exterior Area") {
                float newX = lBorder.transform.position[0] + lBorder.GetComponent<SpriteRenderer>().bounds.size.x/2 - playerCollider.bounds.size.x/2;
                Vector3 temp = new Vector3(this.transform.position.x - newX, 0f, 0f);
                this.transform.position -= temp;
            }

            else if (sceneName == "Kelp Cavern") {
                BaseMovement.spawnPos = new Vector3(-7, 0, 0);
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
                BaseMovement.spawnPos = new Vector3(10, 0, 0);
                SceneManager.LoadScene("Lab1");
            }
        }
        //exterior area lab entrance colliders
        if (collision.gameObject.name == "Lab1")
        {
            BaseMovement.spawnPos = new Vector3(-17, 0, 0);
            SceneManager.LoadScene("Lab1");
        }
        if (collision.gameObject.name == "Lab2")
        {
            BaseMovement.spawnPos = new Vector3(28, 0, 0);
            SceneManager.LoadScene("Lab2");
        }
    }

    private void spawnAtLab() {

        //there is probably a better way to do this
        //but this works for now
        float newX = 0f;
        float newY = 0f;
        
        if (labSpawn == 1) {
            Lab1 lab = FindObjectOfType<Lab1>();
            //x has a slight offset to spawn next to the lab
            newX = lab.transform.position[0] - lab.GetComponent<SpriteRenderer>().bounds.size.x/2 - 5;
            newY = lab.transform.position[1];
        }

        else if (labSpawn == 2) {
            Lab2 lab = FindObjectOfType<Lab2>();
            //x has a slight offset to spawn next to the lab
            newX = lab.transform.position[0] - lab.GetComponent<SpriteRenderer>().bounds.size.x/2 - 5;
            newY = lab.transform.position[1];
            
        }

        Vector3 temp = new Vector3(newX - this.transform.position.x, newY - this.transform.position.y, 0f);
        this.transform.position += temp;
        labSpawn = 0;
    }
}