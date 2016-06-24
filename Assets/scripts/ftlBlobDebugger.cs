//Attach this script to your Main Camera. In the inspector, assign a game object to "Box"
//Always make sure that your scene contains an Empty Game Object named "TouchScript", with the components 
//"TUIO Input" and "Mouse Input" attached. Without this, TouchScript will not recognize input.


using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;
using TouchScript;
    /// <summary>
    /// Visual debugger to show touches as GUI elements.
    /// </summary>
public class ftlBlobDebugger : Manager
{
private Dictionary<int, ITouch> dummies = new Dictionary<int, ITouch>();
private Dictionary<int, GameObject> boxes = new Dictionary<int, GameObject>();
	public GameObject box;


#region Unity methods


private void OnGUI()
{
		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();

}

#endregion

#region touch handlers

public void touchBegin(ITouch touch)
{
	ITouch dummy;
	if (dummies.TryGetValue(touch.Id, out dummy)) return;
    dummies.Add(touch.Id, touch);
	var position = gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touch.Position.x, touch.Position.y, 1f));

	var newBox = Instantiate (box, position, Quaternion.identity) as GameObject;
//		if (touch.Type == "2dblb")
//		{
//			newBox.transform.localScale = new Vector3 (touch.Size.x , touch.Size.y, 1f);
//			var angle = touch.Angle * 360f / (2 * Mathf.PI);
//			newBox.transform.localRotation =   Quaternion.Euler (0f,0f,angle);
//		}
	boxes.Add (touch.Id, newBox);
}

public void touchMoved(ITouch touch)
{

    ITouch dummy;
    if (!dummies.TryGetValue(touch.Id, out dummy)) return;
	var position = gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touch.Position.x, touch.Position.y, 1f));
	var thisBox = boxes[touch.Id];
	thisBox.transform.position = position;
//		if (touch.Type == "2dblb")
//		{
//			thisBox.transform.localScale = new Vector3 (touch.Size.x , touch.Size.y, 1f);
//			var angle = touch.Angle * 360f / (2 * Mathf.PI);
//			thisBox.transform.localRotation =   Quaternion.Euler (0f,0f, - angle);
//		}
}

public void touchEnd(ITouch touch)
{

    ITouch dummy;
    if (!dummies.TryGetValue(touch.Id, out dummy)) return;
    dummies.Remove(touch.Id);
	GameObject testBox;
	if (!boxes.TryGetValue(touch.Id, out testBox)) return;
	Destroy (boxes[touch.Id]);
	boxes.Remove(touch.Id);
}


#endregion
}