using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    public GameObject whoCanPush;
    //public PhysicsMaterial2D mat;
    public float validPush;
    public float invalidPush;
    public void Pushed(GameObject pusher)
    {
        if (pusher == whoCanPush)
        {
            transform.GetComponent<Rigidbody2D>().mass = validPush;
        }
        else
        {
            transform.GetComponent<Rigidbody2D>().mass = invalidPush;
        }
    }
}
