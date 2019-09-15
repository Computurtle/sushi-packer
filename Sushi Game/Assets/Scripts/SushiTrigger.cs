using UnityEngine;

/// <summary>
/// Trigger when sushi pass it (get to end of conveyor)
/// </summary>
public class SushiTrigger : MonoBehaviour
{
    public SushiSpawning spawner; // Variable for spawner game object

    private PlayerHUD hud; // Variable for HUD script

    // Find HUD script
    private void Start()
    {
        hud = FindObjectOfType<PlayerHUD>();
    }

    /// <summary>
    /// Trigger when a collider enters
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // If object is sushi
        if (other.tag == "Sushi")
        {
            // Trigger LoseHealth method in hud
            hud.LoseHealth();
            // Trigger PenaltyScore method in hud
            hud.PenaltyScore();
            // Remove sushi from sushiList in hud
            hud.sushiList.Remove(other.gameObject);
            // Remove from sushiList in spawner
            spawner.sushiList.Remove(other.gameObject);
            // Destroy gameObject
            Destroy(other.gameObject);
        }
    }
}
