using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OpeningSequence : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;

        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Opening Reworked.mp4");

        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
