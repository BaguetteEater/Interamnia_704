using UnityEngine;

/// <summary>
/// Class specifically to deal with input.
/// </summary>
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

    // How quickly the throttle reacts to input.
    private const float THROTTLE_SPEED = 0.5f;

    //To get the ship's velocity
    private Rigidbody rbody;

    // Keep a reference to the ship this is attached to just in case.
    private Spaceship ship;

	public GameObject joystick;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        ship = GetComponent<Spaceship>();
    }

    private void Update()
    {
		
        // pitch = Input.GetAxis("Vertical");
        // yaw = Input.GetAxis("Horizontal");

		//pitch = joystickInput.getPitch();
		//yaw = joystickInput.getYaw();

        strafe = 0.0f;
    }

    /// <summary>
    /// Uses R and F to raise and lower the throttle.
    /// </summary>
    private void UpdateKeyboardThrottle(KeyCode increaseKey, KeyCode decreaseKey)
    {

        // When moving forward, velocity.z value is between [0;+infinite]

        float target = throttle;

        if (Input.GetKey(increaseKey))
            target = 1.0f;
        else if (Input.GetKey(decreaseKey) && this.rbody.velocity.z > 3) // On empeche de reculer si on recule deja ou à l'arret
            target = -1.0f;


        throttle = Mathf.MoveTowards(throttle, target, Time.deltaTime * THROTTLE_SPEED);
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
 }