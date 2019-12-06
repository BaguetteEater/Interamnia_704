using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedalsController : MonoBehaviour
{
    public GameObject spaceship;
	private SpaceshipInput spaceshipInput;
        
    void Start()
    {
		// UnityEngine.XR.InputTracking.disablePositionalTracking = true;
		spaceshipInput = spaceship.GetComponent<SpaceshipInput>();

        try
        {
            Debug.Log(Input.GetAxis("Thruster"));
            Debug.Log("PedalsController successfully initiated.");
        }
        catch (UnityException exc)
        {
            Debug.Log("Missing Thruster Axis!");
        }
    }

    void FixedUpdate()
    {
        float thruster = Input.GetAxis("Thruster");

		spaceshipInput.UpdateThrottle(thruster);
    }
}
