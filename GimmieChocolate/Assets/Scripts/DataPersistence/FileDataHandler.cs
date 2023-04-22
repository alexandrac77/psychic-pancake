using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    // directory path for where to save data.
    private string dataDirPath = "";
    // name of file.
    private string dataFileName = "";

    // Constructor: takes in file values & sets them accordingly.
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        // Use Path.Combine to account for different Operating Systems having different path separators.
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        // variable to load data into.
        GameData loadedData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                // Load serialised data from file.
                // FileMode.Open allows reading from file.
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    // pass in stream just created.
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        // Load serialised data.
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                // Deserialise data from JSON back into C# game data object.
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        // Load game data if it exists
        return loadedData;

    }

    public void Save(GameData data)
    {
        // Use Path.Combine to account for different Operating Systems having different path separators.
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        // Catch errors when saving data to the file.
        try
        {
            // Create directory the file is written to.
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialise the C# game data object into JSON.
            // true is optional & formats the data.
            string dataToStore = JsonUtility.ToJson(data, true);

            // Write serialised data to the file.
            // Using block ensures the connection to the file is closed once we are 
            // done reading & writing to it. FileMode.Create if used for writing to file.
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                // Create StreamWriter, passing in stream created above.
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving data to file: " + fullPath + "\n" + e);
        }

    }
}
