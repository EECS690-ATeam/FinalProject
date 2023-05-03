using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, IDataPersistence
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

    public Animator headAnimator;
    public Transform headWrapper;
    public SpriteRenderer head;

    public float topAngleLimit = 35;
    public float bottomAngleLimit = -20f;
    public Vector2 mouseWorldPos;
    bool facingRight = true;
    
    //controlled for each scene transition
    public static Vector3 spawnPos;
    public static int labSpawn;

    // For Save
    public string Scene = "";

    public void LoadData(GameData data)
    {
        //this.transform.position = data.playerPosition;
        this.Scene = data.scene;
    }

    public void SaveData(GameData data)
    {
        // Problem with scenes having different position
        // data.playerPosition = this.transform.position;
        data.scene = this.Scene;
    }

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
            this.transform.position = new Vector3(spawnPos[0], spawnPos[1], spawnPos[2]);
        }
    }

    private void UpdateCollider() {
        if(animator.GetBool("IsIdle") == false) {
            playerCollider.size = new Vector3(7.4f, 3f);
        }
        else {
            playerCollider.size = new Vector3(1.42f, 7.4f);
        }
    }

    private void LookAtMouse() 
    {
        var dir = (mouseWorldPos - (Vector2) headWrapper.position).normalized;
        headWrapper.right = (dir * Mathf.Sign(transform.localScale.x));
        var eulerDir = headWrapper.localEulerAngles;
        eulerDir.z = Mathf.Clamp(eulerDir.z - (eulerDir.z > 180 ? 360 : 0), bottomAngleLimit, topAngleLimit);
        headWrapper.localEulerAngles = eulerDir; 
    }

    private void FlipX(float x) {
        if(facingRight && x<0) {
            animator.SetBool("SetTurnL", true);
            //headAnimator.SetBool("HeadTurn", true);
            animator.SetBool("Flip", true);
            facingRight = !facingRight;
            // transform.eulerAngles += new Vector3(0,180,0);
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        if(!facingRight && x>0) {
            animator.SetBool("SetTurnR", true);
            //headAnimator.SetBool("HeadTurn", true);
            animator.SetBool("Flip", true);
            facingRight = !facingRight;
            // transform.eulerAngles += new Vector3(0,-180,0);
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        if(x != 0) 
        {
            if(animator.GetBool("SetTurnL") == true || animator.GetBool("SetTurnR") == true) headAnimator.SetBool("HeadTurn", true);
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
        }
    }

    void ShowHead() {
        head.enabled = true;
    }


    void ChangeRotation() {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");
        if(facingRight) {
            if(horizontalMovement!=0 && verticalMovement>0 && GameObject.eulerAngles.z > 180) {
                transform.Rotate(new Vector3(0,0,.3f));
            }
            if(horizontalMovement!=0 && verticalMovement>0 && GameObject.eulerAngles.z < 25) {
                transform.Rotate(new Vector3(0,0,.3f));
            }
            if(!(verticalMovement>0) && GameObject.eulerAngles.z > 0 && GameObject.eulerAngles.z < 30) {
                transform.Rotate(new Vector3(0,0,-.3f));
            }

            if(horizontalMovement!=0 && verticalMovement<0 && GameObject.eulerAngles.z < 180) {
                transform.Rotate(new Vector3(0,0,-.3f));
            }
            if(horizontalMovement!=0 && verticalMovement<0 && (GameObject.eulerAngles.z > 310 || GameObject.eulerAngles.z == 0))  {
                transform.Rotate(new Vector3(0,0,-.3f));
            }
            if(!(verticalMovement<0)  && GameObject.eulerAngles.z < 357 && GameObject.eulerAngles.z > 30) {
                transform.Rotate(new Vector3(0,0,.3f));
            }
        }
        else {
            if(horizontalMovement!=0 && verticalMovement<0 && GameObject.eulerAngles.z > 180) {
                transform.Rotate(new Vector3(0,0,.3f));
            }
            if(horizontalMovement!=0 && verticalMovement<0 && GameObject.eulerAngles.z < 40) {
                transform.Rotate(new Vector3(0,0,.3f));
            }
            if(!(verticalMovement<0) && GameObject.eulerAngles.z > 0 && GameObject.eulerAngles.z < 44) {
                transform.Rotate(new Vector3(0,0,-.3f));
            }

            if(horizontalMovement!=0 && verticalMovement>0 && GameObject.eulerAngles.z < 180) {
                transform.Rotate(new Vector3(0,0,-.3f));
            }
            if(horizontalMovement!=0 && verticalMovement>0 && (GameObject.eulerAngles.z > 335 || GameObject.eulerAngles.z == 0))  {
                transform.Rotate(new Vector3(0,0,-.3f));
            }
            if(!(verticalMovement>0)  && GameObject.eulerAngles.z < 357 && GameObject.eulerAngles.z > 44) {
                transform.Rotate(new Vector3(0,0,.3f));
            }
        }
    }

    void HideHead() {
        head.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(animator.GetBool("SetTurnL") == true || animator.GetBool("SetTurnR") == true) headAnimator.SetBool("HeadTurn", true);
        // if((animator.GetBool("SetTurnL") == true || animator.GetBool("SetTurnR") == true) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Swim") || animator.GetCurrentAnimatorStateInfo(0).IsName("stop") || animator.GetCurrentAnimatorStateInfo(0).IsName("turnLeft") || animator.GetCurrentAnimatorStateInfo(0).IsName("turnRight"))) {
        //     head.enabled = false;
        //     Invoke("ShowHead", 0.4f);
        // }
        UpdateCollider();
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        FlipX(horizontalMovement);
        if(horizontalMovement != 0) animator.SetBool("IsIdle", false);
        else animator.SetBool("IsIdle", true);
        if(horizontalMovement != 0) headAnimator.SetBool("IsIdle", false);
        else headAnimator.SetBool("IsIdle", true);
        transform.position += new Vector3(horizontalMovement, verticalMovement, 0) * Time.deltaTime * MovementSpeed;
        ChangeRotation();
        LookAtMouse();  
    }

        private void OnParticleCollision(GameObject other) { 
            PlayerTakeDmg(1);
            oofSound.Play();
            spawnPos = new Vector3(-26, 0, 0);
            if(GameManager.gameManager._playerHealth.Health == 0) 
            {
                Invoke("resetScene", .4f);
            }
        }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "EcholocationFish")
        {
            PlayerTakeDmg(100);
            oofSound.Play();
            if(GameManager.gameManager._playerHealth.Health == 0)
            {
                spawnPos = new Vector3(-46, 57, 0);
                Invoke("resetScene", .4f);
            }
        }
        if (collision.gameObject.name == "Jellyfish(Clone)")
        {
            PlayerTakeDmg(25);
            oofSound.Play();
            if (GameManager.gameManager._playerHealth.Health == 0)
            {
                spawnPos = new Vector3(-110, 46, 0);
                SceneManager.LoadScene("Cavern1");
            }
        }
    }

    private void resetScene() {
        SceneManager.LoadScene("Cavern1");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        // if(collision.gameObject.name == "EcholocationFish")
        // {
        //     PlayerTakeDmg(25);
        //     oofSound.Play();
        //     spawnPos = new Vector3(-26, 0, 0);
        //     if(GameManager.gameManager._playerHealth.Health == 0) SceneManager.LoadScene("Kelp Cavern");
        // }
    }

    public void JellyfishDamage()
    {
        PlayerTakeDmg(25);
        oofSound.Play();
        spawnPos = new Vector3(-26, 0, 0);
        if (GameManager.gameManager._playerHealth.Health == 0) SceneManager.LoadScene("Kelp Cavern");
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

        else if (labSpawn == 3) {
            Lab3 lab = FindObjectOfType<Lab3>();
            //x has a slight offset to spawn next to the lab
            newX = lab.transform.position[0] - lab.GetComponent<SpriteRenderer>().bounds.size.x/2 - 5;
            newY = lab.transform.position[1];
            
        }

        Vector3 temp = new Vector3(newX - this.transform.position.x, newY - this.transform.position.y, 0f);
        //Debug.Log(temp);
        this.transform.position += temp;
        labSpawn = 0;
    }
}