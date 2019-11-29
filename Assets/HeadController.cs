using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class HeadController : MonoBehaviour
{
	public GameObject cockpit;

    // Start is called before the first frame update

	void Start() 
	{
		UnityEngine.XR.InputTracking.disablePositionalTracking = true;
		this.transform.position = cockpit.transform.position;
	}

    void Update()
    {

    }
}
