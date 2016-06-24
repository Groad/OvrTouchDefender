using UnityEngine;
using System.Collections;
using Ovr;

public class Spawn : Manager 
{
	private float startTime = 0;
	private bool hasFired = false;
	private Vector3 origin;
	public GameObject TsCam;
	public bool ovr = true;

	public GameObject missile;

	void OnEnable () 
	{
		startTime = gameStartTime;
		hasFired = false;
		origin = gameObject.transform.position;
		foreach (var indi in GameObject.FindGameObjectsWithTag("Cali"))
			Destroy (indi);
	}

	void Update () 
	{
		if (hasFired)
		{
			startTime = Time.realtimeSinceStartup;
			hasFired = false;
		}
		if (Time.realtimeSinceStartup - startTime >= 2.0f && !hasFired)
		{
			var newMissile = Instantiate (missile, origin, Quaternion.identity) as GameObject;
			newMissile.GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, -50);
			newMissile.GetComponent<Rigidbody>().useGravity = false;
			hasFired = true;
			var camPos = Camera.main.transform.position;
			var rndX = Random.Range(camPos.x - 8.4f, camPos.x + 8.4f);
			var rndY = Random.Range (camPos.y - 4.39f, camPos.y + 4.39f);
			var rndZ = transform.position.z;
			if (ovr)
			{
				if (Mathf.Abs(OVRManager.display.GetHeadPose().orientation.x) >= 0.5f
				    || Mathf.Abs(OVRManager.display.GetHeadPose().orientation.y) >= 0.5f)
				{
					var headPos = GameObject.Find ("CenterEyeAnchor").transform.position;
					rndX = headPos.x;
					rndY = headPos.y * 0.75f;
				}
			}
			transform.position = new Vector3 (rndX, rndY, rndZ);
			origin = gameObject.transform.position;
		}
	}
}
