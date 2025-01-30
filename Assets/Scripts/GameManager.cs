using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public int currentLives = 3;

    public float respawnTime = 2f;

    public int currentScore;

    private int highScore = 500;

    public bool levelEnding;

    private int levelScore;

    public float waitForLevelEnd = 5f;

    public string nextLevel;

    private bool canPause;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentLives = PlayerPrefs.GetInt("CurrentLives");
        UIManager.Instance.livesText.text = "x " + currentLives;

        

        highScore = PlayerPrefs.GetInt("HighScore");

        UIManager.Instance.hiScoreText.text = "Hi-Score: " + highScore;

        currentScore = PlayerPrefs.GetInt("CurrentScore");
        UIManager.Instance.scoreText.text = "Score: " + currentScore;

        canPause = true;
    }

    void Update()
    {
        if(levelEnding)
        {
            PlayerController.instance.transform.position += new Vector3(PlayerController.instance.boostSpeed * Time.deltaTime, 0f, 0f);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            PauseUnpause();
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
            PlayerPrefs.SetInt("HighScore", highScore);

            canPause = false;

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
            //PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public IEnumerator EndLevelCo()
    {
        UIManager.Instance.levelEndScreen.SetActive(true);
        PlayerController.instance.stopMovement = true;
        levelEnding = true;
        MusicController.instance.PlayVictory();

        canPause = false;

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

        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("CurrentLives", currentLives);

        yield return new WaitForSeconds(waitForLevelEnd);

        SceneManager.LoadScene(nextLevel);
    }

    public void PauseUnpause()
    {
        if(UIManager.Instance.pauseScreen.activeInHierarchy)
        {
            UIManager.Instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.instance.stopMovement = false;
        } else
        {
            UIManager.Instance.pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            PlayerController.instance.stopMovement = true;
        }
    }
}
