using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseMovement : MonoBehaviour, IDataPersistence
{
    public float speed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    bool facingRight=true;

    public static Vector3 spawnPos;
    public Animator anim;

    
    private Collision2D currentCollision;
    // For Save
    public string Scene = "";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        this.transform.position += spawnPos;
        Debug.Log(this.transform.position += spawnPos);
        //this.playerInventory = new Dictionary<ItemData, InventoryItem>();
    }


    public void LoadData(GameData data)
    {
        //this.transform.position = data.playerPosition;
        this.Scene = data.scene;
    }

    public void SaveData(GameData data)
    {
        // data.playerPosition = this.transform.position;
        data.scene = this.Scene;
    }


    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        if(moveInput != 0) anim.SetBool("walk", true);
        else anim.SetBool("walk", false);

        float newspeed = speed;
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        

        if (sceneName == "Lab2") {
            newspeed = speed * 7;
        }
        rb.velocity = new Vector2(moveInput * newspeed, rb.velocity.y);
        if(moveInput<0 && facingRight) {
            facingRight = !facingRight;
            sr.flipX = true;
        }
        if(moveInput>0 && !facingRight) {
            facingRight = !facingRight;
            sr.flipX = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }  
}
