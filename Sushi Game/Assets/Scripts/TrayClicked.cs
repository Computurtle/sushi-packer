using UnityEngine;

/// <summary>
/// Trigger when the tray is clicked
/// </summary>
public class TrayClicked : MonoBehaviour
{
    private SushiClicked tray; // Variable for sushi clicked script

    // Find sushi clicked script
    void Start()
    {
        tray = FindObjectOfType<SushiClicked>();
    }

    // Trigger ClickedTray method when clicked on
    private void OnMouseDown()
    {
        tray.ClickedTray();
    }
}
