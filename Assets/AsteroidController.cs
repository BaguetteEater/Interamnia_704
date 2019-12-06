using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    private AsteroidFactory asteroidFactory;

    private int frames = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        frames++;

        if (asteroidFactory != null && frames == 100 && asteroidFactory.IsOutOfBounds(this.gameObject))
        {
            asteroidFactory.DestroyAsteroid(this.gameObject);
            frames = 0;
        }
    }

    public void SetFactory(ref AsteroidFactory asteroidFactory2)
    {
        this.asteroidFactory = asteroidFactory2;
    }
}
