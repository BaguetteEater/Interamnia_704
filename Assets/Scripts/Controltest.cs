using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controltest : MonoBehaviour
{

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device device
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}
    // Start is called before the first frame update
    void Start()
    {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey("r"))
		{
			UnityEngine.XR.InputTracking.Recenter();
		}
    }
}
