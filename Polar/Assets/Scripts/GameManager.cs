using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int numOfHearts;
    public int numOfFish;
    public int totalFish;
    public string deathMsg;
    private string curentScene;
    public string prevScene;
    public string nextScene;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        curentScene = SceneManager.GetActiveScene().name;
    }

    public void ResetHearts()
    {
        numOfHearts = 3;
    }
    public void Resetfish()
    {
        numOfFish = 0;
    }
    public void GameOver(String msg)
    {
        deathMsg = msg;
        prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("GameOver");
        Resetfish();
        ResetHearts();
    }
    public void GoNextScene()
    {
        prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nextScene);
        ResetHearts();
    }
    public void GoPrevScene()
    {
        nextScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(prevScene);
        Resetfish();
        ResetHearts();
    }
}
