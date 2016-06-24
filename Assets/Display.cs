using UnityEngine;
using System.Collections;

public class Display : Manager
{
	void Start () 
	{

	}
	
	// Update is called once per frame
	void OnEnable () 
	{
		GetComponent<GUIText>().text = "FFUNNNN!!!!";
	}


}
