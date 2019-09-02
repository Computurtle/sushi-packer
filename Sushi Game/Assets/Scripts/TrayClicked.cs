using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayClicked : MonoBehaviour
{
    private SushiClicked tray;

    void Start()
    {
        tray = FindObjectOfType<SushiClicked>();
    }

    private void OnMouseDown()
    {
        tray.ClickedTray();
    }
}
