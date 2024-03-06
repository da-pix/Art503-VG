using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tip : MonoBehaviour
{
    public string tip;
    public GameObject tipBox;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tipBox.SetActive(true);
            tipBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Tip: " + tip;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tipBox.SetActive(false);
        }
    }
}
