using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public float gainRate;
    public float lossRate;
    public int penalty;
    public Image healthBarLeft;
    public Image healthBarRight;
    public bool gameOver = false;
    public GameObject spawner;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI score;
    public TextMeshProUGUI endScore;
    public TextMeshProUGUI scoreTotal;
    public List<GameObject> sushiList;
    public Button start;
    public Button quit;
    public Button howToplay;
    public int bonuses = 0;

    private float startHealth = 100;
    private float currentHealth;
    private float modifier = 0;
    private float timer = 0;

    // Set Health method
    public void SetHealth(float health)
    {
        healthBarLeft.fillAmount = health / 100;
        healthBarRight.fillAmount = health / 100;
        currentHealth = health;
    }

    // Lose health method
    public void LoseHealth()
    {
        float newHealth = currentHealth - lossRate;
        healthBarLeft.fillAmount = newHealth / 100;
        healthBarRight.fillAmount = newHealth / 100;
        currentHealth = newHealth;
    }

    // Penalty method
    public void PenaltyScore()
    {
        modifier -= penalty;
    }

    public void SushiScore(float score)
    {
        modifier += 10 - ((score / 4) * 10);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetHealth(startHealth);
        start.onClick.AddListener(StartClicked);
    }

    void StartClicked()
    {
        start.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        howToplay.gameObject.SetActive(false);
        StartCoroutine(StartGame());
    }

    // Start the Game
    private IEnumerator StartGame()
    {
        // Show text
        yield return new WaitForSeconds(3);
        // Remove text
        healthBarLeft.gameObject.SetActive(true);
        healthBarRight.gameObject.SetActive(true);
        score.gameObject.SetActive(true);
        spawner.GetComponent<SushiSpawning>().playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Update Timer
        if (spawner.GetComponent<SushiSpawning>().playing == true)
        {
            timer += Time.deltaTime;
        }

        // Update score
        float tempScore = timer + modifier;
        score.SetText("Your Score: " + tempScore.ToString("F2"));

        if (gameOver == false)
        {
            // If game is over
            if (currentHealth < 1)
            {
                // Set gameOver to true
                gameOver = true;

                // Stop Spawner
                spawner.GetComponent<SushiSpawning>().playing = false;
                spawner.GetComponent<SushiSpawning>().enabled = false;

                // Make sushi not clickable
                sushiList = spawner.GetComponent<SushiSpawning>().sushiList;
                foreach (GameObject go in sushiList)
                {
                    go.GetComponent<SushiDestroyer>().clickable = false;
                }

                // Set UI texts
                score.gameObject.SetActive(false);
                gameOverText.gameObject.SetActive(true);
                endScore.gameObject.SetActive(true);
                scoreTotal.SetText(tempScore.ToString("F2"));
            }
            // Slowly gain health
            else if (currentHealth < 100)
            {
                SetHealth(currentHealth + (gainRate * Time.deltaTime));
            }
        }
    }
}