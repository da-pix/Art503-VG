using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crackable : MonoBehaviour
{
    public void StartCrack()
    {
        StartCoroutine(Crack());
    }

    private IEnumerator Crack()
    {
        yield return new WaitForSeconds(.5f);
        Debug.Log("cracked Stage: 1");
        yield return new WaitForSeconds(.5f);
        Debug.Log("cracked Stage: 2");
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
