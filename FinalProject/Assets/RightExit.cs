using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RightExit : MonoBehaviour, IDataPersistence
{
    public Transform p;
    private int distance;
    private TextMeshPro popup;
    private bool transitioning;

    public bool kc1Found;
    public bool kc2Found;
    public bool kc3Found;
    public bool kc4Found;
    public bool kc5Found;
    public GameObject playerInventory;



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
            //popup.SetText("Press 'F' to enter");
            if (Input.GetKeyDown("f")) {
                transitioning = true;
                FadeIn.direction = true;
                FadeIn.current = 0f;
                FadeIn.goal = 1f;

                // popup.SetText("");
            }
        }
        else
        {
            //popup.SetText("");
        }
        
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, int> pair in data.simpleDictionary)
        {
            if (pair.Key == "Keycard LVL 1")
            {
                kc1Found = true;
            }
            if (pair.Key == "Keycard LVL 2")
            {
                kc2Found = true;
            }
            if (pair.Key == "Keycard LVL 3")
            {
                kc3Found = true;
            }
            if (pair.Key == "Keycard LVL 4")
            {
                kc4Found = true;
            }
            if (pair.Key == "Keycard LVL 5")
            {
                kc5Found = true;
            }
        }
    }

    public void SaveData(GameData data)
    {

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
            // Check if KeyCard LVL 1 has been collected
            if (kc1Found)
            {
                Debug.Log("Before scene load");
                SceneManager.LoadScene("Cavern1");
            }
        }
        else if (sceneName == "Lab2") {
            PlayerMovement.spawnPos = new Vector3(-60, 36, 0);
            Scene = "Dark Cavern";
            
            SceneManager.LoadScene("Dark Cavern");
            
        }
        else if (sceneName == "Lab3") {
            PlayerMovement.spawnPos = new Vector3(-46, 57, 0);
            Scene = "Jellyfish";
            if (kc4Found) 
            {
                SceneManager.LoadScene("Jellyfish");
            }
        }
        //water -> lab transitions
        else if (sceneName == "Cavern1") {
            BaseMovement.spawnPos = new Vector3(-9, -20, 0);
            Scene = "Lab2";
            if (kc2Found)
            {
                SceneManager.LoadScene("Lab2");
            }
        }
        
        else if (sceneName == "Dark Cavern") {
            BaseMovement.spawnPos = new Vector3(-6, -19, 0);
            Scene = "Lab3";
            if (kc3Found)
            {
                SceneManager.LoadScene("Lab3");
            }
        }

        else if (sceneName == "Jellyfish") {
            BaseMovement.spawnPos = new Vector3(17, -7, 0);
            Scene = "FinalEncounter";
            SceneManager.LoadScene("FinalEncounter");
        }
        
    }

    
}
