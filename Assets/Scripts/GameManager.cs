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

    public bool levelEnding;

    private int levelScore;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UIManager.Instance.livesText.text = "x " + currentLives;

        

        highScore = PlayerPrefs.GetInt("HighScore");

        UIManager.Instance.hiScoreText.text = "Hi-Score: " + highScore;

        currentScore = PlayerPrefs.GetInt("CurrentScore");
        UIManager.Instance.scoreText.text = "Score: " + currentScore;
    }

    void Update()
    {
        if(levelEnding)
        {
            PlayerController.instance.transform.position += new Vector3(PlayerController.instance.boostSpeed * Time.deltaTime, 0f, 0f);
        }
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
        levelScore += scoreToAdd; 
        UIManager.Instance.scoreText.text = "Score: " + currentScore;

        if(currentScore > highScore)
        {
            highScore = currentScore;
            UIManager.Instance.hiScoreText.text = "Hi-Score: " + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public IEnumerator EndLevelCo()
    {
        UIManager.Instance.levelEndScreen.SetActive(true);
        PlayerController.instance.stopMovement = true;
        levelEnding = true;
        MusicController.instance.PlayVictory();

        yield return new WaitForSeconds(.5f);

        UIManager.Instance.endLevelScore.text = "Level Score: " + levelScore;
        UIManager.Instance.endLevelScore.gameObject.SetActive(true);

        yield return new WaitForSeconds(.5f);

        PlayerPrefs.SetInt("CurrentScore", currentScore);
        UIManager.Instance.endCurrentScore.text = "Total Score: " + currentScore;
        UIManager.Instance.endCurrentScore.gameObject.SetActive(true);

        if(currentScore == highScore)
        {
            yield return new WaitForSeconds(.5f);
            UIManager.Instance.highScoreNotice.SetActive(true);
        }
    }
}
