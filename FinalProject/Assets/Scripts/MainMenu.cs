using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System;
using Newtonsoft.Json;
using System.IO;

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
        // Start new game
        Debug.Log("Play game pressed");
        //DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene("Intro Cut Scene");
    }

    public void NewGame()
    {
        // TODO - Clear contents of file save folder
        DataPersistenceManager.instance.NewGame();
    }

    public void LoadGame()
    {
        // TODO - If file does not exist debug, 

        // TODO - If save file exists, Load that file
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
