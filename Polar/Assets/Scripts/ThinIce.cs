using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinIce : MonoBehaviour
{
    public GameObject whoCanBreak;
    public bool isIcey;
    public bool isSnowey;
    public bool slamable;
    AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void BreakCheck(GameObject obj, bool slamming)
    {
        if (obj == whoCanBreak)
        {
            if (slamable)
            {
                if (slamming)
                    StartCoroutine(Break(obj, 0));
                else
                    return;
            }

            else
                StartCoroutine(Break(obj, 0.2f));
        }
    }
    private IEnumerator Break(GameObject obj, float delay)
    {
        if(isIcey)
            audioManager.PlaySFX(audioManager.thinIceCrack);
        else if (isSnowey)
            audioManager.PlaySFX(audioManager.thinSnowBreak);

        yield return new WaitForSeconds(delay);
        obj.GetComponent<PlayerController>().isOnIce = false;
        obj.GetComponent<CollisionDetection>().otherPlayer.GetComponent<PlayerController>().isOnIce = false; // quickfix

        Destroy(gameObject);
    }

}
