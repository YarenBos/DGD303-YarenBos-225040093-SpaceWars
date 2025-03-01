using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string firstlevel;

    public string creditsScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("CurrentLives", 3);
        PlayerPrefs.SetInt("CurrentScore", 0);

        SceneManager.LoadScene(firstlevel);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Leaving the game");
    }

    public void Credits()
    {
        SceneManager.LoadScene(creditsScene);
    }
}
