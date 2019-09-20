using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the players HUD UI
/// </summary>
public class PlayerHUD : MonoBehaviour
{
    public float gainRate; // The rate at which health will increase over time
    public float lossRate; // The rate at which health is taken away
    public Image healthBarLeft; // left health bar image
    public Image healthBarRight; // right health bar image
    public bool gameOver = false; // game over boolean to end scripts at end of game
    public GameObject spawner; // The empty that spawns sushi
    public GameObject sushiRollText; // the empty containing all the letters for text
    public TextMeshProUGUI gameOverText; // Game Over text
    public TextMeshProUGUI score; // Score text
    public TextMeshProUGUI endScore; // End score text
    public TextMeshProUGUI scoreTotal; // end score number
    public TextMeshProUGUI highScore; // text that displays "highscore"
    public TextMeshProUGUI highScoreText; // text that displays the actual highscore
    public List<GameObject> sushiList; // list of sushi currently spawned
    public Button start; // Start button
    public Button quit; // Quit button
    public Button back; // Back button
    public Button howToplay; // How to Play button
    public Button retry; // Retry button
    public Image instructions; // Instructions image
    public Image BG; // background image
    public int bonuses = 0; // Keeps track of the bonus score
    public int penalty; // The amount of score to penalize

    private TextFileReader reader; // Variable that holds the LogReader script
    private float bestScore = 0; // Keeps track of the highest score when using txtFileManager
    private float tempScore = 0; // Keeps track of the score during and at the end of the game
    private float startHealth = 100; // Health the satrt with
    private float currentHealth; // Keeps track of current health
    private float modifier = 0; // Keeps track of bonus + penalized score
    private float timer = 0; // Keeps track of the round timer    
    private Camera cam; // Variable for main camera

    void Start()
    {
        // Start music a little bit slower
        cam = Camera.main;
        cam.GetComponent<AudioSource>().pitch = 0.8f;

        // Set health and add listener for menu buttons
        SetHealth(startHealth);
        reader = GetComponent<TextFileReader>();
        bestScore = reader.LoadFloatByKey("highScore");
        start.onClick.AddListener(StartClicked);
        howToplay.onClick.AddListener(HowToPlayClicked);
        back.onClick.AddListener(BackClicked);
        quit.onClick.AddListener(QuitClicked);
        retry.onClick.AddListener(RetryClicked);
    }

    // Update time, score and check for end game
    void Update()
    {
        // Update Timer
        if (spawner.GetComponent<SushiSpawning>().playing == true)
        {
            timer += Time.deltaTime;
            cam.GetComponent<AudioSource>().pitch *= 1.0001f;
        }

        // Update score
        tempScore = timer + modifier;
        score.SetText("Your Score: " + tempScore.ToString("F2"));

        // if game already isn't over yet
        if (gameOver == false)
        {
            // If health goes below 1
            if (currentHealth < 1)
            {
                EndGame();
            }
            // Slowly gain health
            else if (currentHealth < 100)
            {
                SetHealth(currentHealth + (gainRate * Time.deltaTime));
            }
        }
    }

    /// <summary>
    /// Sets the players health based on parameter
    /// </summary>
    /// <param name="health"></param>
    public void SetHealth(float health)
    {
        healthBarLeft.fillAmount = health / 100;
        healthBarRight.fillAmount = health / 100;
        currentHealth = health;
    }

    /// <summary>
    /// Takes away set amount of health from player
    /// </summary>
    public void LoseHealth()
    {
        float newHealth = currentHealth - lossRate;
        healthBarLeft.fillAmount = newHealth / 100;
        healthBarRight.fillAmount = newHealth / 100;
        currentHealth = newHealth;
    }

    /// <summary>
    /// Penalizes player and adds to penalty score
    /// </summary>
    public void PenaltyScore()
    {
        modifier -= penalty;
    }

    /// <summary>
    /// Adds bonus score to player based on sushi score
    /// </summary>
    /// <param name="score"></param>
    public void SushiScore(float score)
    {
        modifier += 10 - ((score / 4) * 10);
    }

    /// <summary>
    /// Shows help menu
    /// </summary>
    void HowToPlayClicked()
    {
        back.gameObject.SetActive(true);
        instructions.gameObject.SetActive(true);
        BG.gameObject.SetActive(true);
    }

    /// <summary>
    /// Goes back to menu
    /// </summary>
    void BackClicked()
    {
        back.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
        BG.gameObject.SetActive(false);
    }

    /// <summary>
    /// Goes back to menu
    /// </summary>
    void RetryClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Quits application
    /// </summary>
    void QuitClicked()
    {
        Application.Quit();
        Debug.Log("GAME QUIT!");
    }

    /// <summary>
    /// Triggered when health is < 0
    /// </summary>
    void EndGame()
    {
        // Set gameOver to true
        gameOver = true;

        // Stop Spawner
        spawner.GetComponent<SushiSpawning>().playing = false;
        spawner.GetComponent<SushiSpawning>().enabled = false;

        // Make all sushi in sushiList not clickable
        sushiList = spawner.GetComponent<SushiSpawning>().sushiList;
        foreach (GameObject go in sushiList)
        {
            go.GetComponent<SushiDestroyer>().clickable = false;
        }

        // Saves the highscore if beaten
        if (tempScore > bestScore)
        {
            reader.SaveKeyValuePair("highScore", tempScore.ToString("F2"), false);
            highScore.SetText("NEW Highscore");
        }
        else if (bestScore == 0)
        {
            reader.SaveKeyValuePair("highScore", tempScore.ToString("F2"), false);
        }

        // Set score texts text
        highScoreText.SetText(reader.LoadStringByKey("highScore"));
        scoreTotal.SetText(tempScore.ToString("F2"));

        // Set UI texts
        score.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        endScore.gameObject.SetActive(true);
        highScore.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        retry.gameObject.SetActive(true);
    }

    /// <summary>
    /// Setups the start of the game
    /// </summary>
    void StartClicked()
    {
        start.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        howToplay.gameObject.SetActive(false);
        StartCoroutine(StartGame());
    }

    /// <summary>
    /// Starts the game
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartGame()
    {
        // Display "Sushi Roll" text
        sushiRollText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        sushiRollText.gameObject.SetActive(false);
        // Display health bars
        healthBarLeft.gameObject.SetActive(true);
        healthBarRight.gameObject.SetActive(true);
        // Display score
        score.gameObject.SetActive(true);
        // Set bool to true
        spawner.GetComponent<SushiSpawning>().playing = true;
    }
}