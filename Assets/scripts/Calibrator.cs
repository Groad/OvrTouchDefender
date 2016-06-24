using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TouchScript;


//EDITING IN DROPBOX

public class Calibrator : Manager 
{
	public GameObject indicator;
	private Dictionary<int, GameObject> caliPoints = new Dictionary<int, GameObject>();
	private bool yesOrNo = false;
	private bool lSet = false;
	private bool rSet = false;
	private Quaternion upRightQuat = new Quaternion();
	private Quaternion lowLeftQuat = new Quaternion();
	
	private void OnEnable()
	{
		lSet = false;
		rSet = false;
		yesOrNo = false;
	}
	
	public void Calibrate(ITouch touch)
	{
		var upperRightCorner = GetComponent<Camera> ().ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 4f));
		var lowerLeftCorner = GetComponent<Camera> ().ScreenToWorldPoint (new Vector3 (0, 0, 4f));
		var indicatorPosition = GetComponent<Camera> ().ScreenToWorldPoint (new Vector3 (touch.Position.x, touch.Position.y, 4f));
		var upRightQuat = OVRManager.display.GetHeadPose ().orientation;
		var h_ur = upRightQuat.y * Mathf.PI;// * 180f;
		var v_ur = 1.86f - upRightQuat.x * Mathf.PI;// * 180f;
		var surfaceHeight = GameObject.Find ("TouchSurface").transform.position.y;
		var eyePosition = GameObject.Find ("CenterEyeAnchor").transform.position; 
		var eyeHeight = eyePosition.y - surfaceHeight;
		var x_pos = eyeHeight * Mathf.Tan(h_ur);
		var y_pos = eyeHeight * Mathf.Tan(v_ur);
		var position = new Vector3 ();
		position.x = x_pos;
		position.z = y_pos;
		position.y = surfaceHeight;
		if (indicatorPosition.x >= upperRightCorner.x - 0.5f && indicatorPosition.y >= upperRightCorner.y - 0.5f)// && !rSet) 
		{
			upRight = position;
			rSet = true;
			if (lSet)
			{
				isCalibrated = true;
				DoCalibration();
			}
		} 
		else if (indicatorPosition.x <= lowerLeftCorner.x + 0.5f && indicatorPosition.y >= upperRightCorner.y - 0.5f)// && !lSet) 
		{
			lowerLeft = position;
			yesOrNo = true;
			lSet = true;
			lowLeftQuat = OVRManager.display.GetHeadPose ().orientation;
			if (rSet)
			{
				isCalibrated = true;
				DoCalibration ();
			}
		} 
		else
			RemoveOne (touch);
	}
	
	public void AddOne (ITouch touch)
	{
		if (!caliPoints.ContainsKey(touch.Id))
		{
			var position = GetComponent<Camera> ().ScreenToWorldPoint (new Vector3 (touch.Position.x, touch.Position.y, 1));
			var newTouch = Instantiate (indicator, position, Quaternion.identity) as GameObject;
			newTouch.name = "Indicator";
			newTouch.tag = "Cali";
			caliPoints.Add (touch.Id, newTouch);
		}
		
	}
	
	public void MoveOne (ITouch touch)
	{
		var position = GetComponent<Camera> ().ScreenToWorldPoint (new Vector3 (touch.Position.x, touch.Position.y, 1));
		if (caliPoints.ContainsKey (touch.Id))
			caliPoints [touch.Id].transform.position = position;		
	}
	
	public void RemoveOne (ITouch touch)
	{
		if (caliPoints.ContainsKey (touch.Id)) 
		{
			Destroy (caliPoints[touch.Id]);
			caliPoints.Remove(touch.Id);
		}
	}
	
	public void DoCalibration ()
	{
		if (yesOrNo = true)
		{
			var surfaceHeight = GameObject.Find ("TouchSurface").transform.position.y;
			var eyePosition = GameObject.Find ("CenterEyeAnchor").transform.position; 
			var eyeHeight = eyePosition.y - surfaceHeight;
			var screenUpper = GetComponent<Camera>().ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height,0));
			var screenLower = GetComponent<Camera>().ScreenToWorldPoint(new Vector3 (0, 0, 0));
			var screenSize = screenUpper - screenLower;
			var xRatio = (Mathf.Abs (upRight.x) + Mathf.Abs(lowerLeft.x)) / 10;
			var yRatio = (Mathf.Abs (upRight.z) + Mathf.Abs(lowerLeft.z)) / (2 * screenSize.y);

			var tSurface = GameObject.Find ("TouchSurface");
			var currentScale = tSurface.transform.localScale;
			var scaleVector = new Vector3 (xRatio, currentScale.y, xRatio * 9.0f / 16.0f);
			tSurface.transform.localScale = scaleVector;
			gameStartTime = Time.realtimeSinceStartup;
			foreach (var caliPoint in caliPoints)
			{
				Destroy (caliPoint.Value);
			}
			caliPoints.Clear ();
			StartGame ();
			MODE = "Play";
			GameObject.Find ("Spawner").GetComponent<Spawn>().enabled = true;
			//var position = GameObject.Find ("OK").transform.position;
			//position.y = -100f;
			//GameObject.Find ("OK").transform.position = position;
			foreach (var indi in GameObject.FindGameObjectsWithTag("Cali"))
				Destroy (indi);
		}
	}
}
