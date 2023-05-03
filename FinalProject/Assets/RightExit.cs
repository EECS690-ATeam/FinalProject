using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RightExit : MonoBehaviour
{
    public Transform p;
    private int distance;
    private TextMeshPro popup;
    private bool transitioning;

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
    }

    // Update is called once per frame
    void Update()
    {
        float diff = Vector3.Distance(this.transform.position, p.position);
        if (diff < distance && !transitioning)
        {
            popup.SetText("Press 'F' to enter");
            if (Input.GetKeyDown("f")) {
                transitioning = true;
                FadeIn.direction = true;
                FadeIn.current = 0f;
                FadeIn.goal = 1f;
                popup.SetText("");
            }
        }
        else
        {
            popup.SetText("");
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
            PlayerMovement.spawnPos = new Vector3(-110, 46, 0);
            Scene = "Cavern1";
            SceneManager.LoadScene("Cavern1");
        }
        else if (sceneName == "Lab2") {
            PlayerMovement.spawnPos = new Vector3(-60, 36, 0);
            Scene = "Dark Cavern";
            SceneManager.LoadScene("Dark Cavern");
        }
        else if (sceneName == "Lab3") {
            PlayerMovement.spawnPos = new Vector3(-46, 57, 0);
            Scene = "Jellyfish";
            SceneManager.LoadScene("Jellyfish");
        }
        //water -> lab transitions
        else if (sceneName == "Cavern1") {
            BaseMovement.spawnPos = new Vector3(-23, -26, 0);
            Scene = "Lab2";
            SceneManager.LoadScene("Lab2");
        }
        
        else if (sceneName == "Dark Cavern") {
            BaseMovement.spawnPos = new Vector3(-6, -19, 0);
            Scene = "Lab3";
            SceneManager.LoadScene("Lab3");
        }

        else if (sceneName == "Jellyfish") {
            BaseMovement.spawnPos = new Vector3(17, -7, 0);
            Scene = "FinalEncounter";
            SceneManager.LoadScene("FinalEncounter");
        }
        

    }
}
