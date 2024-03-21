using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public TextMeshProUGUI fishText;
    public AudioClip onClickSfx;
    public AudioSource src;
    void Start()
    {
        src.clip = onClickSfx;
        fishText.text = "Fish Collected: \n" + GameManager.Instance.numOfFish.ToString() + " / " + GameManager.Instance.totalFish.ToString();
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
