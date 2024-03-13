using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public Transform subject;

    Vector2 startPos;
    float startZ;

    Vector2 travel => (Vector2) cam.transform.position - startPos;
    float parallaxFactor => Mathf.Abs( distanceFromS) / clippingPlane;

    float distanceFromS => transform.position.z - subject.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFromS > 0 ? cam.farClipPlane : cam.nearClipPlane));

    public void Start()
    {
        startPos = transform.position;
        startZ = transform.position.z;
    }

    public void Update()
    {
        Vector2 newPos = startPos + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }

}
