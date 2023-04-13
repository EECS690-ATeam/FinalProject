using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 150f;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    Vector2 movement;
    LeftBorder lBorder;
    RightBorder rBorder;
    public AudioSource oofSound;

    private SpriteRenderer sr;
    public float verticalRot = 30;
    public float horizontalRot = 180;

    public float MovementSpeed = 15;
    public Transform GameObject;

    [SerializeField] private Image redTinge = null;
    [SerializeField] private float hurtTimer = 0.1f;
    
    public Animator animator;
    //public Animator headAnimator;

    //public Transform headWrapper;
    public float topAngleLimit = 35;
    public float bottomAngleLimit = -20f;
    public Vector2 mouseWorldPos;
    bool facingRight = true;
    
    //controlled for each scene transition
    public static Vector3 spawnPos;
    public static int labSpawn;


    public void PlayerTakeDmg(int dmg) 
    {
        GameManager.gameManager._playerHealth.DmgUnit(dmg);
        UpdateHealth();
        StartCoroutine(HurtFlash());
        Debug.Log(GameManager.gameManager._playerHealth.Health);
    }

    private void PlayerHeal(int healing) 
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
    }

    void UpdateHealth() 
    {
        Color tingeAlpha = redTinge.color;
        tingeAlpha.a = 1 - (GameManager.gameManager._playerHealth.Health / GameManager.gameManager._playerHealth.MaxHealth);
        redTinge.color = tingeAlpha;
    }

    IEnumerator HurtFlash() 
    {
        redTinge.enabled = true;
        yield return new WaitForSeconds(hurtTimer);
        redTinge.enabled = false;
    }

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

    private void LookAtMouse() 
    {
        //var dir = (mouseWorldPos - (Vector2) headWrapper.position).normalized;
        //headWrapper.right = (dir * Mathf.Sign(transform.localScale.x));
        //var eulerDir = headWrapper.localEulerAngles;
        //eulerDir.z = Mathf.Clamp(eulerDir.z - (eulerDir.z > 180 ? 360 : 0), bottomAngleLimit, topAngleLimit);
        //headWrapper.localEulerAngles = eulerDir; 
    }

    private void FlipX(float x) {
        if(facingRight && x<0) {
            animator.SetBool("SetTurnL", true);
            animator.SetBool("Flip", true);
            facingRight = !facingRight;
        }
        if(!facingRight && x>0) {
            animator.SetBool("SetTurnR", true);
            animator.SetBool("Flip", true);
            facingRight = !facingRight;
        }
         if (x!=0) {
            if(animator.GetBool("SetTurnL") == false) transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
            if(animator.GetBool("SetTurnR") == false) transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
         }
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        FlipX(horizontalMovement);
        if(horizontalMovement != 0) animator.SetBool("IsIdle", false);
        else animator.SetBool("IsIdle", true);
        //if(horizontalMovement != 0) headAnimator.SetBool("IsIdle", false);
        //else headAnimator.SetBool("IsIdle", true);
        transform.position += new Vector3(horizontalMovement, verticalMovement, 0) * Time.deltaTime * MovementSpeed;
        LookAtMouse();  
        // // Input
        // movement.x = Input.GetAxisRaw("Horizontal");
        // movement.y = Input.GetAxisRaw("Vertical");


        // rb.velocity = new Vector2(movement.x, movement.y).normalized * moveSpeed;

    }

    //// Not tied to the frame rate like Update() is
    //void FixedUpdate()
    //{
    //    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    //}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "EcholocationFish")
        {
            PlayerTakeDmg(25);
            oofSound.Play();
            spawnPos = new Vector3(-26, 0, 0);
            if(GameManager.gameManager._playerHealth.Health == 0) SceneManager.LoadScene("Kelp Cavern");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        // if(collision.gameObject.name == "EcholocationFish")
        // {
        //     PlayerTakeDmg(25);
        //     oofSound.Play();
        //     spawnPos = new Vector3(-26, 0, 0);
        //     if(GameManager.gameManager._playerHealth.Health == 0) SceneManager.LoadScene("Kelp Cavern");
        // }
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