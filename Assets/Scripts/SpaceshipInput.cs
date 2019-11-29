//
// Copyright (c) Brian Hernandez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
//

using UnityEngine;

/// <summary>
/// Class specifically to deal with input.
/// </summary>
public class SpaceshipInput : MonoBehaviour
{
    [Tooltip("When true, the mouse and mousewheel are used for ship input and A/D can be used for strafing like in many arcade space sims.\n\nOtherwise, WASD/Arrows/Joystick + R/T are used for flying, representing a more traditional style space sim.")]
    public bool usePedalsInput = false;
    [Tooltip("When using Keyboard/Joystick input, should roll be added to horizontal stick movement. This is a common trick in traditional space sims to help ships roll into turns and gives a more plane-like feeling of flight.")]
    public bool addRoll = true;

    [Space]

    [Range(-1, 1)]
    public float pitch;
    [Range(-1, 1)]
    public float yaw;
    [Range(-1, 1)]
    public float roll;
    [Range(-1, 1)]
    public float strafe;
    [Range(0, 1)]
    public float throttle;

    // How quickly the throttle reacts to input.
    private const float THROTTLE_SPEED = 0.5f;

    //To get the ship's velocity
    private Rigidbody rbody;

    // Keep a reference to the ship this is attached to just in case.
    private Spaceship ship;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        ship = GetComponent<Spaceship>();
    }

    private void Update()
    {

        pitch = Input.GetAxis("Vertical");
        yaw = Input.GetAxis("Horizontal");

        if (addRoll)
            roll = -Input.GetAxis("Horizontal") * 0.5f;

        strafe = 0.0f;

        if (usePedalsInput)
            UpdatePedalsThrottle();   
        else
            UpdateKeyboardThrottle(KeyCode.R, KeyCode.F);
    }

    /// <summary>
    /// Freelancer style mouse controls. This uses the mouse to simulate a virtual joystick.
    /// When the mouse is in the center of the screen, this is the same as a centered stick.
    /// </summary>
    private void SetStickCommandsUsingMouse()
    {
        Vector3 mousePos = Input.mousePosition; 

        // Figure out most position relative to center of screen.
        // (0, 0) is center, (-1, -1) is bottom left, (1, 1) is top right.      
        pitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height* 0.5f);
        yaw = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        // Make sure the values don't exceed limits.
        pitch = -Mathf.Clamp(pitch, -1.0f, 1.0f);
        yaw = Mathf.Clamp(yaw, -1.0f, 1.0f);
    }

    /// <summary>
    /// Uses R and F to raise and lower the throttle.
    /// </summary>
    private void UpdateKeyboardThrottle(KeyCode increaseKey, KeyCode decreaseKey)
    {

        // When moving forward, velocity.z value is between [0;+infinite]

        float target = throttle;

        if (Input.GetKey(increaseKey))
            target = 1.0f;
        else if (Input.GetKey(decreaseKey) && this.rbody.velocity.z > 3) // On empeche de reculer si on recule deja ou à l'arret
            target = -1.0f;


        throttle = Mathf.MoveTowards(throttle, target, Time.deltaTime * THROTTLE_SPEED);
    }

    private void UpdatePedalsThrottle()
    {

        // When moving forward, velocity.z value is between [0;+infinite]
        float target = throttle;

        // Pedals's input is expected between -1 and 1
        target = Input.GetAxis("Thruster");

        throttle = Mathf.MoveTowards(throttle, target, Time.deltaTime * THROTTLE_SPEED);
    }

    /// <summary>
    /// Uses the mouse wheel to control the throttle.
    /// </summary>
    private void UpdateMouseWheelThrottle()
    {
        throttle += Input.GetAxis("Mouse ScrollWheel");
        throttle = Mathf.Clamp(throttle, 0.0f, 1.0f);
    }
}