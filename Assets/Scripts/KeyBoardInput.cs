using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInput : MonoBehaviour
{
    private GameObject spaceship;
    private SpaceshipController spaceshipController;

    private int pitch = 0, yaw = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("keyboard");
        spaceship = this.gameObject;
        this.spaceshipController = spaceship.GetComponent<SpaceshipController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(pitch);

        if (Input.GetKeyDown("z")) 
        {
            pitch += 1;
            this.spaceshipController.ChangePitchAxis(pitch);
        } 
        else if (Input.GetKeyUp("z"))
        {
            pitch = 0;
        }

        if (Input.GetKey("q")) 
        {
            yaw -= 1;
            this.spaceshipController.ChangeYawAxis(yaw);
        }
        else if (Input.GetKeyUp("z"))
        {
            yaw = 0;
        }
        
        if (Input.GetKey("s")) 
        {
            pitch -= 1;
            this.spaceshipController.ChangePitchAxis(pitch);
        }
        else if (Input.GetKeyUp("z"))
        {
            pitch = 0;
        }
        
        if (Input.GetKey("d")) 
        {
            yaw += 1;
            this.spaceshipController.ChangeYawAxis(yaw);
        }
        else if (Input.GetKeyUp("z"))
        {
            yaw = 0;
        }
    }
}
