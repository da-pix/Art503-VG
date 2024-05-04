using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CamLocker : MonoBehaviour
{
    public List<Transform> plyrs;
    public Camera cam;
    public Vector3 lockedCamPos;
    public float lockedCamZoom;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log(collision.name);
            if (plyrs.Contains(collision.transform))
            {
                return;
            }
            else
            {
                plyrs.Add(collision.transform);
            }

            if (cam.GetComponent<MultipletargetCamera>().targets.Count == plyrs.Count)
            {
                cam.GetComponent<MultipletargetCamera>().lockedPos = lockedCamPos;
                cam.GetComponent<MultipletargetCamera>().lockedZoom = lockedCamZoom;
                cam.GetComponent<MultipletargetCamera>().lockedCam = true;
            }

        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            plyrs.Remove(collision.transform);

            if ( plyrs.Count == 0)
            {
                cam.GetComponent<MultipletargetCamera>().lockedCam = false;
            }
        }
    }
}
