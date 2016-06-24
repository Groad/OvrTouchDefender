using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public GameObject boom;
	void OnCollisionEnter (Collision incoming)
	{
		if (incoming.gameObject.tag == "missile")
		{
			//Instantiate (boom, transform.position
//			var audioSource = GetComponent<AudioSource> ();
//			var sound = audioSource.clip;
//			audio.PlayOneShot (sound);
		}
	}
}
