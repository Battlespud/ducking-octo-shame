using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

	Vector3 lastFramePosition;

	bool paused = false;

	public List<Fleets> selectedFleets; 
	Sprites sprites;

	float timescaleLast = 1f;
	float targetTimeScale = 1f;

	//these are used to control camera zoom levels
	const int perspZoomInLimit = 5; //minimum distance from plane
	const int perspZoomOutLimit = 50; //max distance from plane
	const int perspZoomSpeed = 15;

	const int orthoZoomInLimit = 5; //smallest size
	const int orthoZoomOutLimit = 45; //max size of orthographic view, bigger is much more intensive
	const int orthoZoomSpeed = 10; //multiplier for mouse input

	// Use this for initialization
	void Start () {
		sprites = GetComponentInParent<Sprites> ();
	}

	// Update is called once per frame
	void Update () {

		Vector3 currFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );  //poll where the mouse is at the start of each frame and store it
		currFramePosition.z = 0; //always keep the mouse 1 layer closer to the camera than our highest layer in order to keep it visible.


		Vector3 diff = new Vector3(0,0,0);  //storage container for the difference between where we were and where we moved the mouse to.

		// Handle screen dragging
		if( Input.GetMouseButton(1) ) {	// Right

			diff = lastFramePosition - currFramePosition;  //determine difference in each vector component
			diff.z = 0; //prevent dragging along the z layer by accident.  Prevents major glitches, do not alter
			Camera.main.transform.Translate( diff ); //move the camera the same amount our mouse moved
		}

		//Handle Camera Zooming via scroll wheel
		if( Input.GetAxis ("Mouse ScrollWheel") != 0f ) {	// zoom function.  Works, but requires a switch to perspective camera and changes to other parts of the script to compoensate.
			switch (Camera.main.orthographic) { //different camera modes handle zooming differently.  Perspective actually moves the camera, while ortho changes the canvas size to simulate it
			case true:
				{	
					float f = Camera.main.orthographicSize - Input.GetAxis ("Mouse ScrollWheel")*orthoZoomSpeed;
					if (f > orthoZoomInLimit && f < orthoZoomOutLimit) {//how close you can zoom in or how far y ou can zoom out
						Camera.main.orthographicSize = f;
					}
					break;
				}
			case false:
				{
					diff.z = Input.GetAxis ("Mouse ScrollWheel")*perspZoomSpeed;
					if ((diff.z < 0 && Camera.main.transform.position.z > perspZoomOutLimit*-1) || (diff.z > 0 && Camera.main.transform.position.z < perspZoomInLimit*-1)) {
						Camera.main.transform.Translate (diff);
					}
					break;
				}
			}
		} //end of zooming



		//Handle Left Mouse Clicks
		if (Input.GetMouseButtonDown(2)) { //on middle mouse button click
			Debug.Log (currFramePosition);
			Instantiate (sprites.FleetPrefab, currFramePosition, Quaternion.identity);
		}

		if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.A)) { //on left mouse button click, selection
			Debug.Log("Raycasting..");
			RaycastHit2D hit = Physics2D.Raycast(new Vector3(currFramePosition.x, currFramePosition.y, -10),Vector3.forward*20, Mathf.Infinity);
			Debug.DrawRay (new Vector3 (currFramePosition.x, currFramePosition.y, -10), Vector3.forward*20, Color.blue,10);
			if (hit.collider != null) {
				Debug.Log ("Hitting " + hit.collider.name);
			} else {
				Debug.Log ("Hit nothing!");
			}
			//dumb way without raycasts
			foreach (Fleets fleet in Fleets.FleetsList) {
				Collider coll = fleet.fleetGo.GetComponent<Collider> ();
				if(coll.bounds.Contains(new Vector3(currFramePosition.x,currFramePosition.y,coll.transform.position.z))){
					changeSelection (fleet);
					Debug.Log ("Selected fleet: " + fleet.fleetName);
				}
			}

		}

		if (Input.GetMouseButtonDown (0) && Input.GetKey(KeyCode.A)) { //on left mouse button click + A key, movement
			foreach (Fleets fleet in selectedFleets) {
				fleet.TargetPosition = currFramePosition;
				Debug.Log (currFramePosition);
			}
		}



		//Timescale stuff_______________________________________________________
		if (Input.GetKeyDown(KeyCode.Equals)) { //on left mouse button click
			targetTimeScale += .25f;
			Debug.Log(Time.timeScale);
		}

		if (Input.GetKeyDown(KeyCode.Minus)) { //on left mouse button click
			targetTimeScale -= .25f;
			Debug.Log(Time.timeScale);
		}

		if (Input.GetKeyDown(KeyCode.Space)) { //on left mouse button click
			if (paused) {
				paused = false;
				Debug.Log ("Unpause");
			} else {
				paused = true;
				Debug.Log ("Pause");
			}
		}

		if(targetTimeScale >= 1.5f) { targetTimeScale = 1.5f;}
		if (targetTimeScale <= .25f) {targetTimeScale = .25f;	}

		if (paused) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = targetTimeScale;
		}

		//______________________________________________________________________

		//TODO add wasd scrolling

		//grab and save the current mouse position to use as a reference for next frame.
		lastFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		lastFramePosition.z = 0;



	
	} //end of update

	void changeSelection(Fleets fleet){
		if(selectedFleets.Contains(fleet)){
			selectedFleets.Remove(fleet);
		}
			else{
				selectedFleets.Add(fleet);
			}
	}



}
