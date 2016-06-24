using UnityEngine;
using System.Collections;

public class MissileScript : MonoBehaviour {

	public GameObject boom;
	public float currentTime;
	// Use this for initialization
	void OnEnable () {
		GetComponent<Rigidbody> ().useGravity = false;
		currentTime = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.position.z <= -2000f)
			Destroy (gameObject);
	
	}

	void OnCollisionEnter (Collision defender)
	{
		if (transform.position.z <= 20)
	    {
			var x = Instantiate (boom, transform.position, Quaternion.identity) as GameObject;

			Destroy (gameObject);
//			var rb = GetComponent<Rigidbody> ();
//			rb.useGravity = true;
//			var vel = rb.velocity;
//			vel.z = -vel.z;
//			rb.velocity = vel;
		}
	}
}
