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
        GameManager.Instance.nextScene = nxtLvl;
        src.clip = onClickSfx;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && creds.activeSelf)
        {
            StartCoroutine(UnFade(fadeDur));
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && selctMenu.activeSelf)
        {
            selctMenu.SetActive(false);
        }
    }

    public void LoadGame()
    {
        src.Play();
        GameManager.Instance.GoNextScene();
    }

    public void oil()
    {
        GameManager.Instance.isPoluted = true;
        GameManager.Instance.playCS = true;
        GameManager.Instance.playTut = true;
        selctMenu.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        selctMenu.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
    }
    public void clean()
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
        StartCoroutine(Fade(fadeDur));
        Debug.Log("Show creds");
    }

    private IEnumerator Fade(float dur)
    {
        float scaler = 1 / fadeDur;
        while (intrface.GetComponent<CanvasGroup>().alpha > 0)
        {
            intrface.GetComponent<CanvasGroup>().alpha -= scaler;

            if (intrface.GetComponent<CanvasGroup>().alpha <= .25)
                intrface.SetActive(false);
            yield return null;

        }
        creds.SetActive(true);
    }
    private IEnumerator UnFade(float dur)
    {
        float scaler = 1 / fadeDur;
        creds.SetActive(false);

        while (intrface.GetComponent<CanvasGroup>().alpha < 1)
        {
            intrface.GetComponent<CanvasGroup>().alpha += scaler;

            if (intrface.GetComponent<CanvasGroup>().alpha >= .25)
                intrface.SetActive(true);
            yield return null;
        }
    }
}
