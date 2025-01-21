using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    private Player player;
    private Spawner spawner;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    public Button retryButton;
    public SpriteRenderer playerDead;

    private float score;

    // using a singleton for the animation speed throughout the game 
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            DestroyImmediate(gameObject);
        }

    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach(var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        enabled = true;
        score = 0f;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        playerDead.gameObject.SetActive(false);

        UpdateHighScore();
    }

    public void GameOver()
    {
        enabled = false;
        gameSpeed = 0f;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        playerDead.gameObject.SetActive(true);

        UpdateHighScore();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;

        // we increase the score based on the game speed, as game is faster -> gain higher score 
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");

    }

    private void UpdateHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("highScore", 0);

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("highScore", highScore);
        }

        highScoreText.text = Mathf.FloorToInt(highScore).ToString("D5");
    }

}
