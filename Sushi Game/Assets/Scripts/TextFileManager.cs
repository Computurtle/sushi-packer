using System.IO;
using UnityEngine;

/// <summary>
/// Manages the interaction between the game and text files
/// </summary>
[System.Serializable]
public class TextFileManager 
{
    public string logName; // Variable that contains the name of the log being used (assigned in inspector)
    public string[] logContents; // Keeps track of the logs that have been made to ensure everything is up to date

    /// <summary>
    /// Method used to create the log, will only create the log if it does not already exist
    /// </summary>
    public void CreateFile(string fileName) 
    {
        // Directory path for log to check
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        // If it does not already exist
        if (File.Exists(dirPath) == false) 
        {
            // Create the directory and write the fileName as first line (will also create text file)
            Directory.CreateDirectory(Application.dataPath + "/Resources");
            File.WriteAllText(dirPath, fileName + "\n");
        }
    }

    /// <summary>
    /// Method used to read file contents
    /// </summary>
    public string[] ReadFileContents(string fileName) 
    {
        // Directory to read txt file from
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        // Initialise tContents array
        string[] tContents = new string[0];
        // if the file looking for exists, read it and save it to tContents
        if (File.Exists(dirPath) == true) 
        {
            tContents = File.ReadAllLines(dirPath);
        }
        // Update logContents
        logContents = tContents;
        // Return tContents
        return tContents;
    }

    // Not currently in use //
    // public void AddFileLine(string fileName, string fileContents)
    // {
    //     ReadFileContents(fileName);
    //     string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
    //     string tContents = fileContents + "\n";
    //     string timestamp = "Time Stamp: " + System.DateTime.Now;
    //     if (File.Exists(dirPath) == true)
    //     {
    //         File.AppendAllText(dirPath, timestamp + " - " + tContents);
    //     }
    // }

    /// <summary>
    /// Method used for saving a new key and value pair
    /// </summary>
    public void AddKeyValuePair(string fileName, string key, string value, bool isTimestamped) 
    {
        // Read file to update logContents
        ReadFileContents(fileName);
        // Directory
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        // Format tContents
        string tContents = key + "," + value + "\n";
        // Format string
        string timestamp = "Time Stamp: " + System.DateTime.Now;
        // If the file exists
        if (File.Exists(dirPath) == true) 
        {
            bool contentsFound = false; // used to keep track of whether content was found or not
            // Iterate through list of keys
            for (int i = 0; i < logContents.Length; i++) 
            {
                // If 1 of the keys is the same
                if (logContents[i].Contains(key) == true) 
                {
                    // Check for if timestamp is true, then update logContents
                    if (isTimestamped == true) 
                    {
                        logContents[i] = timestamp + " - " + tContents;
                    } else {
                        logContents[i] = tContents;
                    }
                    // Set contents found to true
                    contentsFound = true;
                }

                // If content was found
                if (contentsFound == true) {
                    // Add logContents to end of log
                    File.WriteAllLines(dirPath, logContents);
                } else // if the key is not found, create it by appending it to end of log
                {
                    // Check for if timestamp is true, then append text
                    if (isTimestamped == true) 
                    {
                        File.AppendAllText(dirPath, timestamp + " - " + tContents);
                    } else {
                        File.AppendAllText(dirPath, tContents);
                    }
                }
            }
        }
    }

    // Method used to locate a key by a string
    public string LocateStringByKey(string key) 
    {
        // Read log to update logContents
        ReadFileContents(logName);
        string t = "";
        // Iterate through all keys
        foreach (string s in logContents) 
        {
            // If key matches search
            if (s.Contains(key) == true) 
            {
                // Split up the line, and get the last item, which will be the value
                string[] splitString = s.Split (",".ToCharArray ());
                t = splitString[splitString.Length - 1];
            }
        }
        // Return the value
        return t;
    }

    // Try create the file and read contents to update logContents
    public void Start() 
    {
        CreateFile(logName);
        ReadFileContents(logName);
    }
}