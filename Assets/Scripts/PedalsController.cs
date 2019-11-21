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
    }

    void Update()
    {

    }

    // Intensity should be between 0 and 1
    private void OnRightPedalInput(int intensity)
    {
        // ToDo: Move the Spaceship
    }


    private void OnMiddlePedalInput(int intensivity)
    {
        // ToDo: Make the Spaceship brake
    }
}
