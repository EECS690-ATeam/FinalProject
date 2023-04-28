using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;
    public List<ResItem> resolutions = new List<ResItem>();
    private int currentRes;
    private int resCount;
    public TMP_Text resLabel;

    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else 
         {
            vsyncTog.isOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResLeft()
    {
        currentRes--;
        if (currentRes < 0)
        {
            currentRes = 0;
        }
        UpdateLabel();
    }

    public void ResRight() 
    {
        currentRes++;
        resCount = resolutions.Count;
        if (currentRes > resCount - 1)
        {
            currentRes = resCount - 1;
        }
        UpdateLabel();
    }

    public void UpdateLabel() 
    {
        resLabel.text = resolutions[currentRes].horizontal.ToString() + " x " + resolutions[currentRes].vertical.ToString();
    }

    public void ApplySettings()
    {
        Screen.fullScreen = fullscreenTog.isOn;

        if(vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else 
        {
            QualitySettings.vSyncCount = 0;
        }
    }
}

[System.Serializable]
public class ResItem 
{
    public int horizontal, vertical;
}