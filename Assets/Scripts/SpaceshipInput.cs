using System;
using UnityEngine;

public class SpaceshipInput : MonoBehaviour
{
    [Space]

    [Range(-1, 1)]
    public float pitch;
    [Range(-1, 1)]
    public float yaw;
    [Range(-1, 1)]
    public float roll;
    [Range(-1, 1)]
    public float strafe;
    [Range(-1, 1)]
    public float throttle;

    public GameObject laserPrefab;

    // How quickly the throttle reacts to input.
    private const float THROTTLE_SPEED = 0.5f;

    private Rigidbody rigidbody;
    private AudioSource motorSound;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        motorSound = GetComponents<AudioSource>()[1];
        motorSound.loop = true;
        motorSound.Play();
    }

    private void Update()
    {
        strafe = 0.0f;
		motorSound.pitch = Mathf.Abs(rigidbody.velocity.z) / 60;
    }

    public void UpdateThrottle(float input) 
	{
		throttle = Mathf.MoveTowards(throttle, input, Time.deltaTime * THROTTLE_SPEED);
    }

    public void UpdatePitch(float input)
	{
		pitch = input;
	}

	public void UpdateYaw(float input)
	{
		yaw = input;
	}

    public void Fire()
    {
        GameObject laser = Instantiate(
            laserPrefab,
            this.transform.forward + new Vector3(this.transform.rotation.x, 0, 0),
            Quaternion.identity,
            null
        ) as GameObject;

        laser.transform.localRotation = this.transform.rotation * Quaternion.Euler(90, 0, 0);
        laser.GetComponent<Rigidbody>().AddForce(this.transform.forward * 10000);
    }
}