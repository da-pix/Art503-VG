using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinIce : MonoBehaviour
{
    public GameObject whoCanBreak;
    public void BreakCheck(GameObject obj)
    {
        if (obj == whoCanBreak)
        {
            Destroy(gameObject);
        }
    }
}
