using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI deathMsgTxt;
    public AudioClip onClickSfx;
    public AudioSource src;
    void Start()
    {
        src.clip = onClickSfx;
        deathMsgTxt.text = GameManager.Instance.deathMsg;
    }
    public void LoadGame()
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
