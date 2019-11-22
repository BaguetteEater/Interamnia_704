using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedalsController : MonoBehaviour
{
    public GameObject spaceship;
    private SpaceshipController spaceshipController;
    
    void Start()
    {
        this.spaceshipController = spaceship.GetComponent<SpaceshipController>();

        try
        {
            Input.GetAxis("Thruster");
            Debug.Log("PedalsController successfully initiated.");
        }
        catch (UnityException exc)
        {
            Debug.LogError("Missing Thruster Axis!");
        }
    }

    void FixedUpdate()
    {
        float thruster = Input.GetAxis("Thruster");

        if (thruster < 0)
        {
            OnBrakeInput(thruster);
        } else if (thruster > 0)
        {
            OnAcceleratorInput(thruster);
        }
    }

    // Intensity should be between 0 and 1
    private void OnAcceleratorInput(float intensity)
    {
        spaceshipController.forwardSpeed = (uint) Math.Truncate(intensity * 100);
    }


    private void OnBrakeInput(float intensity)
    {
        this.spaceshipController.Decelarate((uint) - Math.Truncate(intensity * 100));
    }
}
