using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMenuController : MonoBehaviour
{
	private SteamVR_TrackedObject hand;
	private SteamVR_Controller.Device device
	{
		get { return SteamVR_Controller.Input((int) hand.index); }
	}

	void Start()
	{
		hand = GetComponent<SteamVR_TrackedObject>();
	}
	
    // Update is called once per frame
    void Update()
    {
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))  
		{
			 Restart();
		}
    }
	
	public void Restart()
	{
		SceneManager.LoadScene("Game");
	}
}
