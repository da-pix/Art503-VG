using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public GameObject collectableFish;
    public GameObject background;
    public GameObject oilStain;
    public Animator water;
    public PlayableDirector cutScene;
    public AudioClip onClickSfx;
    public AudioSource src;
    public Sprite close;
    public Sprite far;
    public AudioSource wind;

    public float elapsedTime;
    public bool timerRunning = false;

    private void Awake()
    {
        if (GameManager.Instance.isPoluted)
        {
            background.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = far;
            background.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = close;
            water.SetBool("isDirty", true);
            oilStain.SetActive(true);
        }
    }
    private void Start()
    {

        if (GameManager.Instance.playCS)
        {
            cutScene.Play();
            
            if (GameManager.Instance.playTut)
                StartCoroutine(PlayTut((float)cutScene.duration));
        }
        else if (!GameManager.Instance.playCS && GameManager.Instance.playTut)
            StartCoroutine(PlayTut(0f));
        else
        {
            timerRunning = true;
            wind.Play();
        }

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
        timerRunning = true;
        wind.Play();
    }

    void Update()
    {
        fishText.text = GameManager.Instance.numOfFish.ToString() + " x ";

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else if(!GameIsPaused && cutScene.state != PlayState.Playing && !tutUI.activeSelf)
            {
                Pause();
            }
        }
        if(GameManager.Instance.playCS && cutScene.state != PlayState.Playing)
        {
            timerRunning = true;
        }

        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            int min = Mathf.FloorToInt(elapsedTime / 60);
            int sec = Mathf.FloorToInt(elapsedTime % 60);
            GameManager.Instance.levelCompleteTime = new Vector2(min, sec);
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
        src.clip = onClickSfx;
        src.Play();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        src.Play();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        src.clip = onClickSfx;
        src.Play();
        Time.timeScale = 1f;
        GameManager.Instance.MainMenu();
    }

    public void QuitGame()
    {
        src.clip = onClickSfx;
        src.Play();
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}
