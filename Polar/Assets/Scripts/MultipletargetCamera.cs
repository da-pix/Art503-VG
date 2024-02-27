using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

// TBH i copied a youtube tutorial so some stuff in here idek
public class MultipletargetCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = .5f;
    public float maxZoom = 10f, minZoom = 40f, zoomLimiter = 50f;
    public float moveUpWhen;

    private Vector3 velocity;
    private Camera cam;
    private float closeOffset;
    private float farOffset;

    private void Start()
    {
        cam = GetComponent<Camera>();
        closeOffset = offset.y ;
        farOffset = offset.y;
    }

    private void FixedUpdate()
    {
        if (targets.Count == 0)
            return;

        Move();
        Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        //Debug.Log(bounds.size.x);
        return bounds.size.x;
    }

    void Move()
    {
        Vector3 centrePoint = getCentrePoint();

        //offset = new Vector3(offset.x, Mathf.Lerp(closeOffset, farOffset, (cam.orthographicSize - 7)/ 2.3f), offset.z);
        

        Vector3 newPosition = centrePoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 getCentrePoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
