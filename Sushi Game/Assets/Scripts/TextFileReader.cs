using UnityEngine;

/// <summary>
/// Manages the interaction between unity gameObjects and the TextFileManager
/// </summary>
public class TextFileReader : MonoBehaviour
{
    public TextFileManager txtFileManager = new TextFileManager(); // Variable for instance of TFM

    // Run the start event on the TFM
    private void Start()
    {
        txtFileManager.Start();
    }

    /// <summary>
    /// Method to save a key and value pair, just calls AddKeyValuePair
    /// </summary>
    public void SaveKeyValuePair(string key, string value, bool isTimestamped)
    {
        txtFileManager.AddKeyValuePair(txtFileManager.logName, key, value, isTimestamped);
    }

    /// <summary>
    /// Load a string by key, just calls LocateStringByKey
    /// </summary>
    public string LoadStringByKey(string key)
    {
        return txtFileManager.LocateStringByKey(key);
    }

    /// <summary>
    /// Same as LocateStringByKey except it returns it as a float
    /// </summary>
    public float LoadFloatByKey(string key)
    {
        float f = 0;
        float.TryParse(txtFileManager.LocateStringByKey(key), out f);
        return f;
    }

    /// <summary>
    /// Same as LocateStringByKey except it returns it as a int
    /// </summary>
    public int LoadIntByKey(string key)
    {
        int i = 0;
        int.TryParse(txtFileManager.LocateStringByKey(key), out i);
        return i;
    }

    /// <summary>
    /// Same as LocateStringByKey except it returns it as a bool
    /// </summary>
    public bool LoadBoolByKey(string key)
    {
        string v = txtFileManager.LocateStringByKey(key);
        if (v.ToLower() == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}