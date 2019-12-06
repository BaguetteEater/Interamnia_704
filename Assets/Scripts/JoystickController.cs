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

	void Start () {
		grabed = false;
		spaceshipInput = spaceship.GetComponent<SpaceshipInput>();
		// initalRotation = this.gameObject.transform.rotation;
	}

	private void OnTriggerEnter(Collider other) 
	{
		Debug.Log ("trigger enter");
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
				device.TriggerHapticPulse(2000);
				device.TriggerHapticPulse(2000);
			}

			if (device.GetHairTriggerUp())
			{
				Debug.Log(gameObject.name + " Grabed = false");
				grabed = false;
				hand = null;
				// this.transform.rotation = this.initalRotation;
			}

			if (grabed) 
			{
				// Move the joystick
				Quaternion rotation = hand.gameObject.transform.rotation;
				this.transform.rotation = new Quaternion(rotation.x + 0.55f, rotation.y, rotation.z, rotation.w);

				// Move the spaceship
				Debug.Log(rotation.x);
				spaceshipInput.UpdatePitch(rotation.x);
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
