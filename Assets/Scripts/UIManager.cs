using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public GameObject gameOverScreen;

    public Text livesText;

    public Slider healthBar, shieldBar;

    public Text scoreText, hiScoreText;

    public GameObject levelEndScreen;

    public Text endLevelScore, endCurrentScore;
    public GameObject highScoreNotice;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMain()
    {

    }
}
