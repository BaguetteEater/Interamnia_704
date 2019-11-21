using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    private Rigidbody rigidbody;

    public uint forwardSpeed { get; set; }
    public int verticalSpeed { get; set; }
    public int horizontalSpeed { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();

        // Spaceship is static on beginning
        this.forwardSpeed = 0;
        this.verticalSpeed = 0;
        this.horizontalSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        MoveHorizontaly();
        MoveVerticaly();
    }

    private void MoveForward()
    {
        this.rigidbody.AddForce(this.transform.forward * this.forwardSpeed);
        // ToDo: Either put the camera into the Spaceship or move it independently
        // ToDo: Play engine sounds
    }
    
    private void MoveVerticaly()
    {
        this.rigidbody.AddTorque(this.transform.right * - this.verticalSpeed);
    }

    private void MoveHorizontaly()
    {
        this.rigidbody.AddTorque(this.transform.forward * - this.horizontalSpeed);
    }
}
