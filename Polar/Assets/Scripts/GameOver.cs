using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI deathMsgTxt;
    void Start()
    {
        deathMsgTxt.text = GameManager.Instance.deathMsg;
    }
    public void LoadGame()
    {
        GameManager.Instance.GoPrevScene();
        //SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}
