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

        float newspeed = speed;
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        

        if (sceneName == "Lab2") {
            newspeed = speed * 7;
        }
        rb.velocity = new Vector2(moveInput * newspeed, rb.velocity.y);
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
        Debug.Log(collision.gameObject.name);
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if (collision.gameObject.name == "RightBorder" && (Input.GetAxisRaw("Horizontal") > 0))
        {
            if (sceneName == "Lab1") {
                PlayerMovement.spawnPos = new Vector3(0, 0, 0);
                Scene = "Cavern1";
                SceneManager.LoadScene("Cavern1");
            }

            else if (sceneName == "Lab2") {
                PlayerMovement.spawnPos = new Vector3(0, 0, 0);
                Scene = "Dark Cavern";
                SceneManager.LoadScene("Dark Cavern");
            }

            else if (sceneName == "Lab3") {
                PlayerMovement.labSpawn = 3;
                Scene = "Exterior Area";
                SceneManager.LoadScene("Exterior Area");
            }
        }
        if (collision.gameObject.name == ("LeftBorder") && (Input.GetAxisRaw("Horizontal") < 0))
        {
            if (sceneName == "Lab1") {
                PlayerMovement.labSpawn = 1;
                Scene = "Exterior Area";
                SceneManager.LoadScene("Exterior Area");
            }

            else if (sceneName == "Lab2") {
                PlayerMovement.spawnPos = new Vector3(64, 0, 0);
                Scene = "Cavern1";
                SceneManager.LoadScene("Cavern1");
            }

            else if (sceneName == "Lab3") {
                PlayerMovement.spawnPos = new Vector3(0, -51, 0);
                Scene = "Dark Area";
                SceneManager.LoadScene("Dark Cavern");
            }
        }
    }
}
