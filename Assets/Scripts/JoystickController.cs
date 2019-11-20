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
		
	// Use this for initialization
	void Start () {
		// trackedObj = GetComponent<SteamVR_TrackedObject>();
		grabed = false;
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
	void Update () {
		if (hand != null) {
			if (device.GetHairTriggerDown())
			{
				Debug.Log(gameObject.name + " Grabed = true ");
				grabed = true;
			}

			if (device.GetHairTriggerUp())
			{
				Debug.Log(gameObject.name + " Grabed = false");
				grabed = false;
			}

			if (grabed) {
				this.transform.Rotate(hand.gameObject.transform.rotation.eulerAngles);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Hand"))
		{
			Debug.Log ("Hand left the trigger");
			hand = null;
		}
	}
}
