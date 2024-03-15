using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private int playersReached = 0;
    public GameObject cam;
    public string nxtLvl;


    private void Start()
    {
        GameManager.Instance.nextScene = nxtLvl;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (cam.GetComponent<MultipletargetCamera>().targets.Contains(collision.transform))
            {
                cam.GetComponent<MultipletargetCamera>().targets.Remove(collision.transform);
                collision.gameObject.SetActive(false);
                playersReached++;
            }
            if (playersReached == 2)
            {
                GameManager.Instance.LevelEnd();
                //GameManager.Instance.LoadScene(GameManager.Instance.nextScene);
                //SceneManager.LoadScene("tut End");
            }
        }
    }
}
