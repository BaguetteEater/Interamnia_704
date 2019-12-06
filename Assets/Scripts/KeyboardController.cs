﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public Spaceship spaceship;
    private SpaceshipInput spaceshipInput;

    // Start is called before the first frame update
    void Start()
    {
        spaceshipInput = spaceship.GetComponent<SpaceshipInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            spaceshipInput.UpdateThrottle(1);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            spaceshipInput.UpdateThrottle(-1);
        }
    }
}
