using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string nxtLvl;
    public AudioClip onClickSfx;
    public AudioSource src;
    public GameObject creds;
    public GameObject intrface;
    public GameObject selctMenu;
    public float fadeDur;

    private void Start()
    {
        GameManager.Instance.playCS = true;
        GameManager.Instance.playTut = true;
        GameManager.Instance.nextScene = nxtLvl;
        src.clip = onClickSfx;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && creds.activeSelf)
        {
            intrface.SetActive(true);
            creds.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && selctMenu.activeSelf)
        {
            selctMenu.SetActive(false);
        }
    }

    public void LoadGame()
    {
        src.Play();
        GameManager.Instance.ResetDeathCount();
        GameManager.Instance.GoNextScene();
    }

    public void Oil()
    {
        GameManager.Instance.isPoluted = true;
        GameManager.Instance.playCS = true;
        GameManager.Instance.playTut = true;
        selctMenu.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        selctMenu.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
    }
    public void Clean()
    {
        GameManager.Instance.playCS = false;
        GameManager.Instance.isPoluted = false;
        GameManager.Instance.playTut = true;
        selctMenu.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        selctMenu.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    }

    public void ShowSelect()
    {
        src.Play();
        selctMenu.SetActive(true);
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
        intrface.SetActive(false);
        creds.SetActive(true);
        //StartCoroutine(Fade(fadeDur));
        Debug.Log("Show creds");
    }
}
