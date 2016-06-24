using UnityEngine;
using System.Collections;

public class ScaleTable : Manager {
	private Vector3 newScale = new Vector3();
	private float scaleSpeed = 0.998f;
	private void OnGUI()
	{
		if (Input.GetKey (KeyCode.Minus))
		{
			var oldScale = transform.localScale;
			newScale.x = oldScale.x * scaleSpeed;
			newScale.y = oldScale.y * scaleSpeed;
			newScale.z = oldScale.z * scaleSpeed;
			transform.localScale = newScale;
		}
		if (Input.GetKey (KeyCode.Equals))
		{
			var oldScale = transform.localScale;
			newScale.x = oldScale.x / scaleSpeed;
			newScale.y = oldScale.y / scaleSpeed;
			newScale.z = oldScale.z / scaleSpeed;
			transform.localScale = newScale;
		}
	}
}
