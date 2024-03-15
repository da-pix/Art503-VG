using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI fishText;
    public GameObject hearts;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject tutUI;
    public bool playTut;
    public GameObject collectableFish;
    public bool playCS;
    public PlayableDirector cutScene;

    private void Start()
    {
        if (playCS)
        {
            cutScene.Play();
            
            if (playTut)
                StartCoroutine(PlayTut((float)cutScene.duration));
        }
        else if (!playCS && playTut)
            StartCoroutine(PlayTut(0f));



        GameManager.Instance.totalFish = collectableFish.transform.childCount;
    }

    private IEnumerator PlayTut( float starDelay)
    {
        yield return new WaitForSeconds(starDelay);
        Time.timeScale = 0f;
        tutUI.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        tutUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        fishText.text = GameManager.Instance.numOfFish.ToString() + " x ";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }


    }

    public void LoseHeart()
    {
        if (GameManager.Instance.numOfHearts == 1)
        {
            GameManager.Instance.GameOver("Ran out of lives");
        }
        else hearts.transform.GetChild(GameManager.Instance.numOfHearts - 1).gameObject.SetActive(false);
        GameManager.Instance.numOfHearts--;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.MainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}
