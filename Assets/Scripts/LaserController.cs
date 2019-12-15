using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private const int TIME_OF_LIFE = 1000;
    private int frames = 0;

    void FixedUpdate()
    {
        frames++;

        if (frames >= TIME_OF_LIFE)
        {
            Destroy(this.gameObject);
        }
    }
}
