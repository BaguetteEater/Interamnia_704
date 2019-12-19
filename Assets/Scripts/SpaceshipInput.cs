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
    private int laserAmmo;
    private const int maxLaserAmmo = 10;

    // How quickly the throttle reacts to input.
    private const float THROTTLE_SPEED = 0.5f;

    private Rigidbody rigidbody;
    private AudioSource motorSound;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        laserAmmo = maxLaserAmmo;

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
        if (laserAmmo > 0)
        {
            GameObject laser = Instantiate(
                laserPrefab,
                this.gameObject.transform.position + new Vector3(this.gameObject.transform.rotation.x, 0, 0),
                Quaternion.identity,
                null
            ) as GameObject;

            laser.transform.rotation = this.transform.rotation * Quaternion.Euler(90, 0, 0);
            laser.GetComponent<Rigidbody>().AddRelativeForce(this.transform.forward * 10000);
        }

        laserAmmo--;
    }

    public void Reload()
    {
        laserAmmo = maxLaserAmmo;
    }
}