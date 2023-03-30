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
        vid.Play();
    }

    public void Update()
    {
   

        if (!vid.isPlaying)
        {
            SceneManager.LoadScene(3);
        }
        
    }
}
