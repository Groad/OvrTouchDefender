using UnityEngine;
using System.Collections;
using Ovr;

public class CaliManager : MonoBehaviour {

	public static Quaternion orientation;
	public static OVRTracker thisTracker;
	// Use this for initialization
	void Start () 
	{
		var x = OVRManager.display.GetHeadPose ().orientation;
		thisTracker = OVRManager.tracker;
	}
	
	// Update is called once per frame
	void OnGUI () 
	{
		orientation = OVRManager.display.GetHeadPose ().orientation;

		print (orientation.ToString ());
	}
}
