using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI fishText;
    public GameObject hearts;

    void Update()
    {
        fishText.text = GameManager.Instance.numOfFish.ToString() + " x ";
    }

    public void LoseHeart()
    {
        if (GameManager.Instance.numOfHearts == 1)
        {
            GameManager.Instance.Resetfish();
            GameManager.Instance.ResetHearts();
            SceneManager.LoadScene("Game Over");
        }
        else hearts.transform.GetChild(GameManager.Instance.numOfHearts - 1).gameObject.SetActive(false);
        GameManager.Instance.numOfHearts--;
    }
}
