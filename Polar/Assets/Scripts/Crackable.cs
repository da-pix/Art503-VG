using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crackable : MonoBehaviour
{
    public bool isIcey;
    public float timeDelay;
    private GameObject crack1;
    private GameObject crack2;
    private TilemapRenderer tm;
    AudioManager audioManager;
    private bool isCracking;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        tm = GetComponent<TilemapRenderer>();
        crack1 = transform.GetChild(0).gameObject;
        crack2 = transform.GetChild(1).gameObject;

    }
    public void StartCrack()
    {
        if (!isCracking)
            StartCoroutine(Crack());
    }

    private IEnumerator Crack()
    {
        isCracking = true;
        yield return new WaitForSeconds(timeDelay / 3);
        crack1.SetActive(true);
        audioManager.PlaySFX(audioManager.crack1);
        yield return new WaitForSeconds(timeDelay / 3);
        crack1.SetActive(false);
        crack2.SetActive(true);
        audioManager.PlaySFX(audioManager.crack2);
        yield return new WaitForSeconds(timeDelay / 3);
        audioManager.PlaySFX(audioManager.crack3);
        Destroy(gameObject);
    }
}
