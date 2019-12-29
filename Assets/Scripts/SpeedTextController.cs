using System;
using UnityEngine;

public class SpeedTextController : MonoBehaviour
{
    // private SpaceshipInput spaceshipInput;
    private TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        // spaceshipInput = GetComponentInParent<SpaceshipInput>();
        textMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime * 3600;
        // float throttle = spaceshipInput.throttle * 1000;
        float appliedForce = GetComponentInParent<Rigidbody>().velocity.magnitude;
        int speed = (int) Math.Abs(time * appliedForce);
        textMesh.text = speed + "";
    }
}
