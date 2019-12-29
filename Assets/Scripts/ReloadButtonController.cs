using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadButtonController : MonoBehaviour
{
    private SteamVR_TrackedObject hand;
    private SteamVR_Controller.Device device
    {
        get { return SteamVR_Controller.Input((int)hand.index); }
    }

    public GameObject spaceship;
    private SpaceshipInput spaceshipInput;

    void Start()
    {
        spaceshipInput = spaceship.GetComponent<SpaceshipInput>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log("[Button] Hand Triggered");
            GameObject grabbingOther = other.gameObject;
            hand = grabbingOther.GetComponent<SteamVR_TrackedObject>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log("[Button] Hand left the trigger");
            hand = null;
        }
    }

    private void Update()
    {
        if (hand && device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            spaceshipInput.Reload();
            device.TriggerHapticPulse(1000);
        }
    }
}
