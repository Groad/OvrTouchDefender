using UnityEngine;
using System.Collections;

public class DRT_OculusControl : MonoBehaviour {

	void OnEnable () 
	{
	 
	}

	void OnGUI () 
	{
		if (Input.GetKey (KeyCode.Space))
			OVRManager.display.RecenterPose ();
	}
}
