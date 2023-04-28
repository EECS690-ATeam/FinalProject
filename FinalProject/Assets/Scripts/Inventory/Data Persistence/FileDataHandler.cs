using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    // name of file to save to
    private string dataFileName = "";

    // constructor
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        Debug.Log("FileDataHandler LOAD");
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        // variable to load into 
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                // deserialize
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occurred when trying to laod data from file: " + fullPath + "\n" + e); 
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        Debug.Log("FileDataHandler SAVE");
        // Path.combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // creating directory file will be written to if doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            // serialize game data object into json
            string dataToStore = JsonUtility.ToJson(data, true);
            // write data to file; "using" ensure Filestream is closed once done writing
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }
}
