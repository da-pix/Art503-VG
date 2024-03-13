using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string nxtLvl;
    public AudioClip onClickSfx;
    public AudioSource src;

    private void Start()
    {
        GameManager.Instance.nextScene = nxtLvl;
    }

    public void LoadGame()
    {
        src.clip = onClickSfx;
        src.Play();
        GameManager.Instance.GoNextScene();
        //SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame()
    {
        src.clip = onClickSfx;
        src.Play();
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
    public void DisplayCredits()
    {
        src.clip = onClickSfx;
        src.Play();
        Debug.Log("Show creds");
    }
}
