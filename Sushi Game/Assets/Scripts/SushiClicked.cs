using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the tray filling up and being clicked
/// </summary>
public class SushiClicked : MonoBehaviour
{
    public int index = 0; // Keeps track of how many sushi have been clicked
    public ParticleSystem particles; // Variable for particle system game object
    public GameObject hud; // Variable for HUD game object
    public List<GameObject> trayList = new List<GameObject>(); // List of sushi in the tray

    /// <summary>
    /// Triggered when a sushi on the conveyor has been clicked
    /// </summary>
    public void ClickedSushi()
    {
        if (index < 6)
        {
            // Sets the sushi in the [index] spot of the list to active
            trayList[index].gameObject.SetActive(true);
            index++;
        }
    }

    /// <summary>
    /// Triggered when the collider around the tray has been clicked
    /// </summary>
    public void ClickedTray()
    {
        // Unless the tray is full, do nothing
        if (index == 6)
        {
            // Play the particles once
            particles.Play();
            // Deactivate all the sushi in the tray
            for (int i = 0; i < trayList.Count; i++) trayList[i].gameObject.SetActive(false);
            // Add bonus points
            hud.GetComponent<PlayerHUD>().bonuses += 10;
            // Reset index counter
            index = 0;
        }
    }
}
