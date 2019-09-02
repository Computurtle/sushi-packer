using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiTrigger : MonoBehaviour
{
    public SushiSpawning spawner;

    private PlayerHUD hud;

    private void Start()
    {
        hud = FindObjectOfType<PlayerHUD>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sushi")
        {
            hud.LoseHealth();
            hud.PenaltyScore();
            hud.sushiList.Remove(other.gameObject);
            spawner.sushiList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
