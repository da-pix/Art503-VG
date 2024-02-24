using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crackable : MonoBehaviour
{
    public bool isIcey;
    public float timeDelay;
    public void StartCrack()
    {
        StartCoroutine(Crack());
    }

    private IEnumerator Crack()
    {
        yield return new WaitForSeconds(timeDelay / 3);
        Debug.Log("cracked Stage: 1");
        yield return new WaitForSeconds(timeDelay / 3);
        Debug.Log("cracked Stage: 2");
        yield return new WaitForSeconds(timeDelay / 3);
        Destroy(gameObject);
    }
}
