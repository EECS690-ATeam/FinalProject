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
    public GameObject buttonsScreen;

    public void NewGame()
    {
        // create a new game - which will initialize our game data
        DataPersistenceManager.instance.NewGame();
        // load the gameplay scene - which will in turn save the game because of
        // OnSceneUnloaded() in the DataPersistenceManager
        SceneManager.LoadScene("Intro Cut Scene");
    }

    public void LoadGame()
    {
        // load the next scene - which will in turn load the game because of 
        // OnSceneLoaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("Lab1");
        // SceneManager.LoadScene("Intro Cut Scene");
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
