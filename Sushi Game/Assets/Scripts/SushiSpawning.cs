using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiSpawning : MonoBehaviour
{
    public bool playing;
    public float difficulty;
    public float multiplier;
    public float spawnRate;
    public GameObject sushiPrefab;
    public List<GameObject> sushiList;

    private float spawnTimer = 0;
    private GameObject spawner;

    /// <summary> 
    /// Update the difficulty variable *= 1 + (multiplier * Time.deltaTime)
    /// </summary>
    private void DifficultyUpdate()
    {
        difficulty *= 1 + (multiplier * Time.deltaTime);
    }

    void Start()
    {
        spawner = gameObject;
    }

    /// <summary> 
    /// Spawns a single sushi prefab at the coordinates of the Sushi Spawner, 
    /// will generate a random 'y' axis between -0.5f, 0.5f
    /// </summary>
    private void SpawnSushi()
    {
        if (playing == true)
        {
            Vector3 position = new Vector3(spawner.transform.position.x, spawner.transform.position.y, spawner.transform.position.z + Random.Range(-0.5f, 0.5f));
            GameObject sushi = Instantiate(sushiPrefab, position, sushiPrefab.transform.rotation);
            sushi.GetComponent<SushiDestroyer>().spawner = this;
            sushiList.Add(sushi);
        }
    }

    /// <summary> 
    /// Check if playing = true <br />
    ///  > Execute DifficultyUpdate() method <br />
    ///  > Set spawnRate variable to 1 devided by the current difficulty <br />
    ///  > Update spawnTimer variable until its greater than the current spawnRate variable <br />
    ///  > > Execute SpawnSushi() method and reset spawnTimer variable <para />
    /// Check if sushiList has greater than 0 items <br />
    ///  > For each Sushi GameObject in the sushiList <br />
    ///  > > Move along the 'x' axis at a rate += difficulty * Time.deltaTime
    /// </summary>
    void Update()
    {
        if (playing == true) {
            // Update difficulty
            DifficultyUpdate();
            // Spawn sushi based on difficulty
            spawnRate = 1 / difficulty;
            // Spawn Timer
            if (spawnTimer < spawnRate) spawnTimer += Time.deltaTime;
            else
            {
                SpawnSushi();
                spawnTimer = 0;
            }
        }

        if(sushiList.Count > 0)
        {
            foreach(GameObject go in sushiList)
            {
                Vector3 pos = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
                pos.x += difficulty * Time.deltaTime;
                go.transform.position = pos;
            }
        }
    }
}
