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

    private void Awake()
    {
        tm = GetComponent<TilemapRenderer>();
        crack1 = transform.GetChild(0).gameObject;
        crack2 = transform.GetChild(1).gameObject;

    }
    public void StartCrack()
    {
        StartCoroutine(Crack());
    }

    private IEnumerator Crack()
    {
        yield return new WaitForSeconds(timeDelay / 3);
        tm.enabled = false;
        crack1.SetActive(true);
        Debug.Log("cracked Stage: 1");
        yield return new WaitForSeconds(timeDelay / 3);
        crack1.SetActive(false);
        crack2.SetActive(true);
        Debug.Log("cracked Stage: 2");
        yield return new WaitForSeconds(timeDelay / 3);
        Destroy(gameObject);
    }
}
