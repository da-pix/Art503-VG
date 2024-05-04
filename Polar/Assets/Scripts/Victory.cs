using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public AudioClip onClickSfx;
    public AudioSource src;
    public PlayableDirector cutScene;

    void Start()
    {
        GameManager.Instance.playCS = false;
        GameManager.Instance.playTut = false;
        src.clip = onClickSfx;
        GameManager.Instance.nextScene = "Tutorial";

        if (GameManager.Instance.isPoluted)
        {
            cutScene.Play();
        }
    }
    public void Replay()
    {
        GameManager.Instance.ResetDeathCount();
        GameManager.Instance.GoNextScene();
    }
    public void LoadMenu()
    {
        src.clip = onClickSfx;
        src.Play();
        GameManager.Instance.MainMenu();
    }
}
