using UnityEngine;

public class SushiDestroyer : MonoBehaviour
{
    public SushiSpawning spawner;
    public bool clickable = true;

    private SushiClicked tray;
    private PlayerHUD hud;

    private void Start()
    {
        tray = FindObjectOfType<SushiClicked>();
        hud = FindObjectOfType<PlayerHUD>();
    }

    void OnMouseDown()
    {
        // Destroy game object
        if (clickable == true && tray.index < 6)
        {
            spawner.sushiList.Remove(gameObject);
            Destroy(this.gameObject);
            tray.ClickedSushi();
            hud.SushiScore(transform.position.x + 2);
        }
    }
}