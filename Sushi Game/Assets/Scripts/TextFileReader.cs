using UnityEngine;

/// <summary>
/// Manages the interaction between unity gameObjects and the TextFileManager
/// </summary>
public class TextFileReader : MonoBehaviour
{
    public TextFileManager txtFileManager = new TextFileManager();

    private void Start()
    {
        txtFileManager.Start();
    }

    public void SaveKeyValuePair(string key, string value)
    {
        txtFileManager.AddKeyValuePair(txtFileManager.logName, key, value);
    }

    public string LoadStringByKey(string key)
    {
        return txtFileManager.LocateStringByKey(key);
    }

    public float LoadFloatByKey(string key)
    {
        float f = 0;
        float.TryParse(txtFileManager.LocateStringByKey(key), out f);
        return f;
    }

    public int LoadIntByKey(string key)
    {
        int i = 0;
        int.TryParse(txtFileManager.LocateStringByKey(key), out i);
        return i;
    }

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