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
        Invoke("TransitionToGame", 50);
    }

    public void TransitionToGame()
    {
        SceneManager.LoadScene(5);
    }
}
