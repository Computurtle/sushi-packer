using UnityEngine;

/// <summary>
/// Manages the sushi being destroyed when clicked and updating sushi clicked class
/// </summary>
public class SushiDestroyer : MonoBehaviour
{
    public SushiSpawning spawner; // Variable for spawner game object
    public bool clickable = true; // Variable to declare whether or not they are clickable 

    private SushiClicked tray; // Variable for sushi clicked script
    private PlayerHUD hud; // Variable for sushi clicked script

    /// <summary>
    /// Finds the tray and hud scripts
    /// </summary>
    private void Start()
    {
        tray = FindObjectOfType<SushiClicked>();
        hud = FindObjectOfType<PlayerHUD>();
    }

    /// <summary>
    /// Triggers when the sushi was clicked on by the mouse
    /// </summary>
    void OnMouseDown()
    {
        // Destroys game object if clickable and tray is not full
        if (clickable == true && tray.index < 6)
        {
            // Remove gameObject from the sushiList
            spawner.sushiList.Remove(gameObject);
            // Destroy the instance
            Destroy(this.gameObject);
            // Trigger the clicked sushi method in the trays script
            tray.ClickedSushi();
            // Trigger the sushi score method in the HUDs script
            hud.SushiScore(transform.position.x + 2);
        }
    }
}