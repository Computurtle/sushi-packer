using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the spawning of sushi intances
/// </summary>
public class SushiSpawning : MonoBehaviour
{
    public bool playing; // keeps track of if the game is playing or not
    public float difficulty; // keeps track of the difficulty value
    public float multiplier; // keeps track of the multiplier value
    public float spawnRate; // keeps track of the spawn rate of the sushi
    public GameObject sushiPrefab; // Variable for the sushi Prefab
    public List<GameObject> sushiList; // keeps track of all spawned sushi

    private float spawnTimer = 0; // keeps track of the timer that spawns sushi
    private GameObject spawner; // Variable for the gameObject this script is attached to

    // Set the spawner variable to the gameObject
    void Start()
    {
        spawner = gameObject;
    }

    /// <summary> 
    /// If playing is true, try trigger the SpawnSushi method
    /// </summary>
    void Update()
    {
        if (playing == true) {
            // Update difficulty
            DifficultyUpdate();
            // Spawn rate for sushi based on difficulty
            spawnRate = 1 / difficulty;
            // Spawn Timer check
            if (spawnTimer < spawnRate) spawnTimer += Time.deltaTime;
            else
            {
                // Spawn sushi and reset to 0
                SpawnSushi();
                spawnTimer = 0;
            }
        }

        // Move all sushi in list accross conveyor 
        if(sushiList.Count > 0)
        {
            // Iterate through the list
            foreach(GameObject go in sushiList)
            {
                // Get current pos
                Vector3 pos = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
                // Adjust based on difficulty
                pos.x += difficulty * Time.deltaTime;
                // Move to new pos
                go.transform.position = pos;
            }
        }
    }

    /// <summary> 
    /// Update the difficulty variable *= 1 + (multiplier * Time.deltaTime)
    /// </summary>
    private void DifficultyUpdate()
    {
        difficulty *= 1 + (multiplier * Time.deltaTime);
    }

    /// <summary> 
    /// Spawns a single sushi prefab when triggered
    /// </summary>
    private void SpawnSushi()
    {
        // If playing is true
        if (playing == true)
        {
            // Get a random coord along spawner
            Vector3 position = new Vector3(spawner.transform.position.x, spawner.transform.position.y, spawner.transform.position.z + Random.Range(-0.5f, 0.5f));
            // Create instance of prefab
            GameObject sushi = Instantiate(sushiPrefab, position, sushiPrefab.transform.rotation);
            // Set spawner variable in script on sushi and set it to this
            sushi.GetComponent<SushiDestroyer>().spawner = this;
            // Add new sushi to list
            sushiList.Add(sushi);
        }
    }
}
