using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowController : MonoBehaviour
{
    public float maxXVel, maxEmission, stormForceMax;
    private float startEmission, startXVelMax, startXVelMin;

    public ParticleSystem snow;
    public AreaEffector2D storm;

    public bool isStorming;
    private bool ramping = false;
    private float startTime;




    private void Awake()
    {
        startEmission = snow.emission.rateOverTime.constant;
        startXVelMax = snow.velocityOverLifetime.x.constantMax;
        startXVelMin = snow.velocityOverLifetime.x.constantMin; ;

    }

    // Update is called once per frame
    private IEnumerator RampUp()
    {
        // Check if the storm has started and gradually increase the X velocity
        while (ramping)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / 3f); // Ensure t stays between 0 and 1 over 3 seconds

            var velOverTime = snow.velocityOverLifetime;
            var emission = snow.emission;

            var xVel = new ParticleSystem.MinMaxCurve();
            xVel.mode = ParticleSystemCurveMode.TwoConstants;
            xVel.constantMin = Mathf.Lerp(startXVelMin, maxXVel, t); // Interpolate between min and max X velocity
            xVel.constantMax = Mathf.Lerp(startXVelMax, maxXVel, t);
            velOverTime.x = xVel;
            emission.rateOverTime = Mathf.Lerp(startEmission, maxEmission, t);

            storm.forceMagnitude = Mathf.Lerp(0, stormForceMax, t);
            // Check if 3 seconds have passed, if so, stop increasing velocity
            if (elapsedTime >= 3f)
            {
                ramping = false;
            }
            yield return null;
        }
    }
    private IEnumerator RampDown()
    {
        // Check if the storm has started and gradually increase the X velocity
        while (ramping)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / 3f); // Ensure t stays between 0 and 1 over 3 seconds

            var velOverTime = snow.velocityOverLifetime;
            var emission = snow.emission;

            var xVel = new ParticleSystem.MinMaxCurve();
            xVel.mode = ParticleSystemCurveMode.TwoConstants;
            xVel.constantMin = Mathf.Lerp(maxXVel, startXVelMin, t); // Interpolate between min and max X velocity
            xVel.constantMax = Mathf.Lerp(maxXVel,startXVelMax , t);
            velOverTime.x = xVel;
            emission.rateOverTime = Mathf.Lerp(maxEmission, startEmission, t);

            storm.forceMagnitude = Mathf.Lerp(stormForceMax, 0, t);

            // Check if 3 seconds have passed, if so, stop increasing velocity
            if (elapsedTime >= 3f)
            {
                ramping = false;
            }
            yield return null;
        }
    }
    public void StartStorm()
    {
        ramping = true;
        startTime = Time.time;
        StartCoroutine(RampUp());
    }
    public void EndStorm()
    {
        ramping = true;
        startTime = Time.time;
        StartCoroutine(RampDown());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isStorming)
        {
            isStorming = true;
            StartStorm();
            Debug.Log("start snowstorm");

            // if ((storm.colliderMask & (1 << collision.gameObject.layer)) != 0)
            // {

            //}
        }
    }
}
