using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Animator anim;

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
        Resetfish();
        ResetHearts();
        StartCoroutine(ChangeLvlFade("GameOver"));
    }

    public void MainMenu()
    {
        Resetfish();
        ResetHearts();
        StartCoroutine(ChangeLvlFade("MainMenu"));
    }

    public void LevelEnd()
    {
        prevScene = SceneManager.GetActiveScene().name;
        StartCoroutine(ChangeLvlFade("LevelEnd"));
    }

    IEnumerator ChangeLvlFade( String nxtlvl)
    {
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nxtlvl);
        UnFade();
    }

    public void UnFade()
    {
        anim.SetBool("Fade", false);
    }

    public void GoNextScene()
    {
        prevScene = SceneManager.GetActiveScene().name;
        Resetfish();
        ResetHearts();
        StartCoroutine(ChangeLvlFade(nextScene));
    }
    public void GoPrevScene()
    {
        nextScene = SceneManager.GetActiveScene().name;
        Resetfish();
        ResetHearts();
        StartCoroutine(ChangeLvlFade(prevScene));
    }
}
