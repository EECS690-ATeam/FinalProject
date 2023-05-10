using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutScene : MonoBehaviour
{
    public VideoPlayer vid;
    public GameObject videoPlayer;

    public void Start()
    {
        Invoke("TransitionToGame", 40);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Lab1", LoadSceneMode.Single);
        }
    }

    public void TransitionToGame()
    {
        SceneManager.LoadScene("Lab1", LoadSceneMode.Single);
    }
}
