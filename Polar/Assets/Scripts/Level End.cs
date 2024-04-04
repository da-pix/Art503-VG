using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public TextMeshProUGUI fishText;
    public TextMeshProUGUI timerText;
    public AudioClip onClickSfx;
    public AudioSource src;
    void Start()
    {
        timerText.text = string.Format("Finished in: " + "{0:00}:{1:00}", GameManager.Instance.levelCompleteTime.x, GameManager.Instance.levelCompleteTime.y);
        src.clip = onClickSfx;
        fishText.text = "Fish Collected: \n" + GameManager.Instance.numOfFish.ToString() + " / " + GameManager.Instance.totalFish.ToString();
        GameManager.Instance.playTut = false;
    }
    public void Nextlvl()
    {
        src.Play();
        GameManager.Instance.GoNextScene();
    }

    public void Replay()
    {
        src.Play();
        GameManager.Instance.GoPrevScene();
    }
    public void QuitGame()
    {
        src.Play();
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}
