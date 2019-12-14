using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour {
	private SteamVR_TrackedObject hand;
	private SteamVR_Controller.Device device
	{
		get { return SteamVR_Controller.Input((int) hand.index); }
	}
	private bool grabed;

	public GameObject spaceship;
	private SpaceshipInput spaceshipInput;
	private Quaternion initialRotation;

	void Start () {
		grabed = false;
		spaceshipInput = spaceship.GetComponent<SpaceshipInput>();
		initialRotation = this.gameObject.transform.localRotation;
	}

	private void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag("Hand"))
		{
			Debug.Log ("Hand Triggered");
			GameObject grabbingOther = other.gameObject;
			hand = grabbingOther.GetComponent<SteamVR_TrackedObject>();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (hand != null) 
		{
			if (device.GetHairTriggerDown())
			{
				Debug.Log(gameObject.name + " Grabed = true ");
				grabed = true;
				device.TriggerHapticPulse(5000);
			}

			if (device.GetHairTriggerUp())
			{
				Debug.Log(gameObject.name + " Grabed = false");
				grabed = false;
				hand = null;
				this.gameObject.transform.localRotation = initialRotation;
			}

			if (grabed) 
			{
				// Move the joystick
				Quaternion rotation = hand.gameObject.transform.localRotation;

				this.gameObject.transform.localRotation = new Quaternion(
					rotation.x + 0.55f,
					0, 
					rotation.z,
					rotation.w
				);

				// Move the spaceship
				spaceshipInput.UpdatePitch(this.gameObject.transform.localRotation.x / 100);
				spaceshipInput.UpdateYaw(- this.gameObject.transform.localRotation.z / 100);
		
			}
		}
	}

	void Update()
	{
		if (grabed) 
		{
			if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) 
			{
				spaceshipInput.Fire();
				device.TriggerHapticPulse(1000);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Hand"))
		{
			Debug.Log ("Hand left the trigger");
			//hand = null;
		}
	}
}
