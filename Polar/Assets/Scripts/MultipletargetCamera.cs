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
    private bool rideAdj;
    private float closeOffset;
    private float farOffset;

    private void Start()
    {
        cam = GetComponent<Camera>();
        closeOffset = offset.y;
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
        float yOffset;

        if (targets.Count > 1 && GetGreatestDistance() < 3f)
        {
            yOffset = Mathf.Lerp(1, 2.5f, GetGreatestDistance() / zoomLimiter);

        }
        else
            yOffset = Mathf.Lerp(2.5f, 3.75f, GetGreatestDistance() / zoomLimiter);

        offset = new Vector3(offset.x, yOffset, offset.z);




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
