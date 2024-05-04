using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI deathMsgTxt;
    public TextMeshProUGUI deathCountTxt;

    public AudioClip onClickSfx;
    public AudioSource src;
    void Start()
    {
        GameManager.Instance.playCS = false;
        GameManager.Instance.playTut = false;
        src.clip = onClickSfx;
        deathMsgTxt.text = GameManager.Instance.deathMsg;
        deathCountTxt.text = "Total deaths: " + GameManager.Instance.totalDeaths;
    }
    public void LoadGame()
    {
        src.Play();
        GameManager.Instance.GoPrevScene();
    }
    public void LoadMenu()
    {
        src.Play();
        GameManager.Instance.MainMenu();
    }
}
