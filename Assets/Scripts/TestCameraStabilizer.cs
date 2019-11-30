using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraStabilizer : MonoBehaviour
{
	private Vector3 InitPos;
	public GameObject HMD;
	public Transform CameraWantedPos;
    // Start is called before the first frame update
    void Start()
    {
		InitPos = this.transform.position;
    }

    void LateUpdate()
    {
		transform.position = CameraWantedPos.position - HMD.transform.localPosition;
    }
}
