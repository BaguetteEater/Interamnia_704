using System;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    private Rigidbody rigidbody;

    public uint forwardSpeed { get; set; } // Should be between 0 and 100
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

    public void Decelarate(uint amount)
    {

        if (this.rigidbody.velocity.magnitude > amount)
        {
            this.rigidbody.AddForce(- this.transform.forward * amount);
        }
        else
        {
            this.rigidbody.velocity = Vector3.zero;
        }

    }

    private void MoveForward()
    {
        this.rigidbody.AddForce(this.transform.forward * this.forwardSpeed);
        this.forwardSpeed = 0;

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
