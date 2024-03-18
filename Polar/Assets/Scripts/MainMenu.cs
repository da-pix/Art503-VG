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
        src.clip = onClickSfx;
    }

    public void LoadGame()
    {
        src.Play();
        GameManager.Instance.GoNextScene();
    }
    public void QuitGame()
    {
        src.Play();
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
    public void DisplayCredits()
    {
        src.Play();
        Debug.Log("Show creds");
    }
}
