using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indoors : MonoBehaviour
{
    public Effector2D storm;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            storm.colliderMask &= ~(1 << collision.gameObject.layer);


            Debug.Log(collision.name + " entered");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        storm.colliderMask |= (1 << collision.gameObject.layer);
        Debug.Log(collision.name + " left");


    }
}
