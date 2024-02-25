using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string nxtLvl;
    private void Start()
    {
        GameManager.Instance.nextScene = nxtLvl;
    }

    public void LoadGame()
    {
        GameManager.Instance.GoNextScene();
        //SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
    public void DisplayCredits()
    {
        Debug.Log("Show creds");
    }
}
