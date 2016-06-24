using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TouchScript;

public class Manager : MonoBehaviour 
{
	public static string MODE = "Calibration";
	private GameObject tsCam;
	private Calibrator calibrator;
	private ftlBlobDebugger blobDebug;
	public static float gameStartTime = 0;
	public static Vector3 upRight = new Vector2(0,0);
	public static Vector3 lowerLeft = new Vector2(0,0);
	public static Dictionary<int, ITouch> visiTouches = new Dictionary<int, ITouch>();
	public static bool isCalibrated = false;
	public static Vector3 initialTableScale = new Vector3();
	public static Vector3 Cali_UR = new Vector3();
	public static Vector3 Cali_LL = new Vector3();

	// Use this for initialization
	private void OnEnable()
	{
		tsCam = GameObject.Find ("TS Camera");
		blobDebug = tsCam.GetComponent<ftlBlobDebugger> ();
		calibrator = tsCam.GetComponent<Calibrator> ();
		initialTableScale = GameObject.Find ("TouchSurface").transform.localScale;
		if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan += touchesBeganHandler;
			TouchManager.Instance.TouchesEnded += touchesEndedHandler;
			TouchManager.Instance.TouchesMoved += touchesMovedHandler;
			TouchManager.Instance.TouchesCancelled += touchesCancelledHandler;
		}
	}
	
	private void OnDisable()
	{
		if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan -= touchesBeganHandler;
			TouchManager.Instance.TouchesEnded -= touchesEndedHandler;
			TouchManager.Instance.TouchesMoved -= touchesMovedHandler;
			TouchManager.Instance.TouchesCancelled -= touchesCancelledHandler;
		}
	}

	private void OnGUI()
	{
		if (Input.GetKey (KeyCode.X))
		{
			MODE = "Calibration";
			var position = GameObject.Find ("OK").transform.position;
			position.y = 10.2f;
			GameObject.Find ("OK").transform.position = position;
			isCalibrated = false;
			GameObject.Find ("Spawner").GetComponent<Spawn>().enabled = false;
			GameObject.Find ("TABLE").transform.localScale = initialTableScale;
		}
	}
	
	// Update is called once per frame


	#region Event handlers
	
	private void touchesBeganHandler(object sender, TouchEventArgs e)
	{
		foreach (var touch in e.Touches)
		{
			if (MODE == "Play")
				blobDebug.touchBegin(touch);
			else 
			{
				if (touch.Hit != null && touch.Hit.Transform.name == "OK" && isCalibrated)
				{
					calibrator.DoCalibration();
				}
				else
					calibrator.AddOne(touch);
			}
		}
	}
	
	private void touchesMovedHandler(object sender, TouchEventArgs e)
	{
		foreach (var touch in e.Touches)
		{
			if (MODE == "Play")
				blobDebug.touchMoved(touch);
			else
			{
				calibrator.MoveOne(touch);
			}
		}
	}
	
	private void touchesEndedHandler(object sender, TouchEventArgs e)
	{
		foreach (var touch in e.Touches)
		{
			if (MODE == "Play")
				blobDebug.touchEnd(touch);
			else
			{
				calibrator.Calibrate(touch); 
			}
		}
	}
	
	private void touchesCancelledHandler(object sender, TouchEventArgs e)
	{
		touchesEndedHandler(sender, e);
	}

	public void StartGame()
	{
		//var spawner = GameObject.Find ("Spawner");
		//spawner.GetComponent<Spawn> ().enabled = true;
		foreach (var indi in GameObject.FindGameObjectsWithTag("Cali"))
			Destroy (indi);
	}
	
	#endregion
}
