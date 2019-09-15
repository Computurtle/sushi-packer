using UnityEngine;
using System.IO;

[System.Serializable]
public class TextFileManager
{
    public string logName;
    public string[] logContents;

    public void CreateFile(string fileName)
    {
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        if (File.Exists(dirPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources");
            File.WriteAllText(dirPath, fileName + "\n");
        }
    }

    public string[] ReadFileContents(string fileName)
    {
        string dirPath = Application.dataPath + "/Resources" + fileName + ".txt";
        string[] tContents = new string[0];
        if (File.Exists(dirPath) == true)
        {
            tContents = File.ReadAllLines(dirPath);
        }
        logContents = tContents;
        return tContents;
    }

    public void AddFileLine(string fileName, string fileContents)
    {
        ReadFileContents(fileName);
        string dirPath = Application.dataPath + "/Resources" + fileName + ".txt";
        string tContents = fileContents + "\n";
        string timeStamp = "Time Stamp: " + System.DateTime.Now;
        if (File.Exists(dirPath) == true)
        {
            File.AppendAllText(dirPath, timeStamp + " - " + tContents);
        }
    }

    public void AddKeyValuePair(string fileName, string key, string value)
    {
        ReadFileContents(fileName);
        string dirPath = Application.dataPath + "/Resources" + fileName + ".txt";
        string tContents = key + "," + value;
        string timeStamp = "Time Stamp: " + System.DateTime.Now;
        if (File.Exists(dirPath) == true)
        {
            bool contentsFound = false;
            for (int i = 0; i < logContents.Length; i++)
            {
                if (logContents[i].Contains(key) == true)
                {
                    logContents[i] = timeStamp + " - " + tContents;
                    contentsFound = true;
                }

                if (contentsFound == true)
                {
                    File.WriteAllLines(dirPath, logContents);
                }
                else
                {
                    File.AppendAllText(dirPath, timeStamp + " - " + tContents);
                }
            }
        }
    }

    public string LocateStringByKey(string key)
    {
        ReadFileContents(logName);
        string t = "";
        foreach (string s in logContents)
        {
            if (s.Contains(key) == true)
            {
                string[] splitString = s.Split(",".ToCharArray());
                t = splitString[splitString.Length - 1];
            }
        }
        return t;
    }

    public void Start()
    {
        CreateFile(logName);
        ReadFileContents(logName);
    }
}
