using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiClicked : MonoBehaviour
{
    public int index = 0;
    public GameObject sushi1; public GameObject sushi2; public GameObject sushi3;
    public GameObject sushi4; public GameObject sushi5; public GameObject sushi6;
    public ParticleSystem particles;
    public GameObject hud;

    private List<GameObject> trayList = new List<GameObject>();

    private void Start()
    {
        trayList.Add(sushi1);
        trayList.Add(sushi2);
        trayList.Add(sushi3);
        trayList.Add(sushi4);
        trayList.Add(sushi5);
        trayList.Add(sushi6);
    }

    public void ClickedSushi()
    {
        if (index < 6)
        {
            trayList[index].gameObject.SetActive(true);
            index++;
        }
    }

    public void ClickedTray()
    {
        if (index == 6)
        {
            particles.Play();
            for (int i = 0; i < trayList.Count; i++) trayList[i].gameObject.SetActive(false);
            hud.GetComponent<PlayerHUD>().bonuses += 10;
            index = 0;
        }
    }
}
