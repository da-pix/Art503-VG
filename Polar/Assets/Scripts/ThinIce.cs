using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinIce : MonoBehaviour
{
    public GameObject whoCanBreak;
    public bool isIcey;
    AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void BreakCheck(GameObject obj)
    {
        if (obj == whoCanBreak)
        {
            StartCoroutine(Break(obj));
        }
    }
    private IEnumerator Break(GameObject obj)
    {
        audioManager.PlaySFX(audioManager.thinIceCrack);

        yield return new WaitForSeconds(.2f);
        obj.GetComponent<PlayerController>().isOnIce = false;
        Destroy(gameObject);
    }

}
