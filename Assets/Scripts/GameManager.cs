using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public int currentLives = 3;

    public float respawnTime = 2f;

    public int currentScore;

    private int highScore = 500;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UIManager.Instance.livesText.text = "x " + currentLives;

        UIManager.Instance.scoreText.text = "Score: " + currentScore;

        highScore = PlayerPrefs.GetInt("HighScore");

        UIManager.Instance.hiScoreText.text = "Hi-Score: " + highScore;
    }

    void Update()
    {
        
    }

    public void KillPlayer()
    {
        currentLives--;
        UIManager.Instance.livesText.text = "x " + currentLives;

        if (currentLives > 0)
        {
            //respawn code
            StartCoroutine(RespawnCo());

        }
        else
        {
            //game over code
            UIManager.Instance.gameOverScreen.SetActive(true);
            WaveManager.instance.canSpawnWaves = false;

            MusicController.instance.PlayGameOver();

        }
    }

    public IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(respawnTime);
        HealthManager.instance.Respawn();

        WaveManager.instance.ContinueSpawning();
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        UIManager.Instance.scoreText.text = "Score: " + currentScore;

        if(currentScore > highScore)
        {
            highScore = currentScore;
            UIManager.Instance.hiScoreText.text = "Hi-Score: " + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
