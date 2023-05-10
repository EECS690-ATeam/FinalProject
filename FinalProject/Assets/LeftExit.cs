using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LeftExit : MonoBehaviour
{
    public Transform p;
    private double distance;
    private TextMeshPro popup;
    private bool transitioning;
    private SpriteRenderer sr;
    private Color onColor;
    private Color offColor;

    // For Save
    public string Scene = "";

    // Start is called before the first frame update
    void Start()
    {
        transitioning = false;
        if (SceneManager.GetActiveScene().name == "Lab2") {
            distance = 10;
        }
        else {
            distance = 5;
        }
        popup = GetComponent<TextMeshPro>();
        sr = GetComponent<SpriteRenderer>();
        onColor = new Color(1f, 1f, 1f, 1f);
        offColor = new Color(1f, 1f, 1f, 0f);
        sr.color = offColor;
    }

    // Update is called once per frame
    void Update()
    {
        float diff = Vector3.Distance(this.transform.position, p.position);
        if (diff < distance && !transitioning)
        {
            sr.color = onColor;
            //popup.SetText("Press 'F' to enter");
            if (Input.GetKeyDown("f")) {
                transitioning = true;
                FadeIn.direction = false;
                FadeIn.current = 0f;
                FadeIn.goal = 1f;
               // popup.SetText("");
            }
        }
        else
        {
            sr.color = offColor;
            // popup.SetText("");
        }
        
    }

    public void Transition() {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Debug.Log(currentScene.name);
        if (sceneName == "Exterior Area") {
            SceneManager.LoadScene("Start Menu");
        }
        Debug.Log(this.transform.name);
        //lab -> water transitions
        if (sceneName == "Lab1") {
            PlayerMovement.labSpawn = 1;
            Scene = "Exterior Area";
            SceneManager.LoadScene("Exterior Area");
        }
        else if (sceneName == "Lab2") {
            PlayerMovement.spawnPos = new Vector3(-40, 39, 0);
            Scene = "Cavern1";
            SceneManager.LoadScene("Cavern1");
        }
        else if (sceneName == "Lab3") {
            PlayerMovement.spawnPos = new Vector3(52, -10, 0);
            Scene = "Dark Cavern";
            SceneManager.LoadScene("Dark Cavern");
        }
        //water -> lab transitions
        else if (sceneName == "Cavern1") {
            BaseMovement.spawnPos = new Vector3(20, 0, 0);
            Scene = "Lab1";
            SceneManager.LoadScene("Lab1");
        }
        else if (sceneName == "Dark Cavern") {
            BaseMovement.spawnPos = new Vector3(76, -20, 0);
            Scene = "Lab2";
            SceneManager.LoadScene("Lab2");
        }
        else if (sceneName == "Jellyfish") {
            BaseMovement.spawnPos = new Vector3(50, -19, 0);
            Scene = "Lab3";
            SceneManager.LoadScene("Lab3");
        }
    }
}
