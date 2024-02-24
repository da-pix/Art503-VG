using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public TextMeshProUGUI fishText;
    void update()
    {
        fishText.text = "Fish Collected: \n" + GameManager.Instance.numOfFish.ToString();
    }
    public void LoadGame()
    {
        GameManager.Instance.Resetfish();

        SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}
