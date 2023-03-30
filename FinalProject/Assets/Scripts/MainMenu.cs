using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsScreen;
    public GameObject creditsScreen;
    public GameObject visualsScreen;
    public GameObject buttonsScreen;
    public GameObject videoCanvas;
    public VideoPlayer videoPlayer;
    public GameObject vp;

    public void PlayGame()
    {
        visualsScreen.SetActive(false);
        buttonsScreen.SetActive(false);
        videoCanvas.SetActive(true);
        //videoPlayer.PlayCutscene();
        vp.SetActive(true);
    }

    
    public void OpenOptions()
    {
        buttonsScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
        buttonsScreen.SetActive(true);
    }

    public void OpenCredits()
    {
        buttonsScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsScreen.SetActive(false);
        buttonsScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }
}
