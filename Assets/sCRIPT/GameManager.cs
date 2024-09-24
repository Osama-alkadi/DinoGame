using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    private PlayerMovement player;
    private Spawner spawner;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hightScore;
    public Button uiButton;
    private float score;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        spawner = FindObjectOfType<Spawner>();
        NewGame();
    }
    public void NewGame()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach(var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        gameSpeed = initialGameSpeed;
        enabled = true;
        score = 0;
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        uiButton.gameObject.SetActive(false);
        UpdateHighScore();
    }
    public void GameOver()
    {
        gameSpeed = 0;
        enabled = false;
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        uiButton.gameObject.SetActive(true);
        UpdateHighScore();
    }
    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.RoundToInt(score).ToString("D5");
    }
    public void UpdateHighScore()
    {
        float highscsore = PlayerPrefs.GetFloat("hightScore",0);
        if (score > highscsore)
        {
            highscsore = score;
            PlayerPrefs.SetFloat("hightScore", highscsore);
        }
        hightScore.text = Mathf.RoundToInt(highscsore).ToString("D5");
    }
}
