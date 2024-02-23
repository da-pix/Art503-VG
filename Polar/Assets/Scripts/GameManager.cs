using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI fishText;
    public int fishCount;
    public GameObject hearts;

     void Update()
    {
        fishText.text = fishCount.ToString() + " x ";
    }

    public void LoseLife()
    {
        if (hearts.transform.GetChild(2).gameObject.activeSelf == true)
        {
            hearts.transform.GetChild(2).gameObject.SetActive(false);
        } else if (hearts.transform.GetChild(1).gameObject.activeSelf == true)
        {
            hearts.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (hearts.transform.GetChild(0).gameObject.activeSelf == true)
        {
            SceneManager.LoadScene("Game Over");
        }

    }
}
