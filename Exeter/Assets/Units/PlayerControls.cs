using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerControls : NetworkBehaviour {

	Vector3 lastFramePosition;
	Lists lists;
	TimeController timeController;

	public Camera cam;

	List<Fleets> FleetsList;
	public List<Fleets> selectedFleets; 
	public Fleets[] selFleetArray; //debugging only

	Sprites sprites;

	NetworkPlayer netPlay;

	//these are used to control camera zoom levels
	const int perspZoomInLimit = 5; //minimum distance from plane
	const int perspZoomOutLimit = 50; //max distance from plane
	const int perspZoomSpeed = 15;

	const int orthoZoomInLimit = 10; //smallest size
	const int orthoZoomOutLimit = 200; //max size of orthographic view, bigger is much more intensive
	const int orthoZoomSpeed = 25; //multiplier for mouse input

	// Use this for initialization
	void Start () {
		timeController = GameObject.FindGameObjectWithTag ("Time").GetComponent<TimeController>();
		netPlay = GetComponentInParent<NetworkPlayer> ();
		cam = GetComponentInParent<NetworkPlayer> ().playerCam;

		sprites = GetComponentInParent<Sprites> ();
		FleetsList = GameObject.FindGameObjectWithTag ("Lists").GetComponent<Lists> ().FleetsList;
	}

	// Update is called once per frame
	void Update () {

		//Check if this is the correct player
		if (!isLocalPlayer) {
			return; //aborts if not the correct player
		}

		Vector3 currFramePosition = cam.ScreenToWorldPoint( Input.mousePosition );  //poll where the mouse is at the start of each frame and store it
		currFramePosition.z = 0; //always keep the mouse 1 layer closer to the camera than our highest layer in order to keep it visible.
		//be very careful changing this as it can break selection


		Vector3 diff = new Vector3(0,0,0);  //storage container for the difference between where we were and where we moved the mouse to.

		// Handle screen dragging
		if( Input.GetMouseButton(1) ) {	// Right

			diff = lastFramePosition - currFramePosition;  //determine difference in each vector component
			diff.z = 0; //prevent dragging along the z layer by accident.  Prevents major glitches, do not alter
			cam.transform.Translate( diff ); //move the camera the same amount our mouse moved
		}

		//Handle Camera Zooming via scroll wheel
		if( Input.GetAxis ("Mouse ScrollWheel") != 0f ) {	// zoom function.  Works, but requires a switch to perspective camera and changes to other parts of the script to compoensate.
			switch (cam.orthographic) { //different camera modes handle zooming differently.  Perspective actually moves the camera, while ortho changes the canvas size to simulate it
			case true:
				{	
					float f = cam.orthographicSize - Input.GetAxis ("Mouse ScrollWheel")*orthoZoomSpeed;
					if (f > orthoZoomInLimit && f < orthoZoomOutLimit) {//how close you can zoom in or how far y ou can zoom out
						cam.orthographicSize = f;
					}
					break;
				}
			case false:
				{
					diff.z = Input.GetAxis ("Mouse ScrollWheel")*perspZoomSpeed;
					if ((diff.z < 0 && cam.transform.position.z > perspZoomOutLimit*-1) || (diff.z > 0 && cam.transform.position.z < perspZoomInLimit*-1)) {
						cam.transform.Translate (diff);
					}
					break;
				}
			}
		} //end of zooming



		//Handle Left Mouse Clicks
		if (Input.GetKeyDown(KeyCode.F)) { //on middle mouse button click
			Debug.Log (currFramePosition);
			Instantiate (sprites.FleetPrefab, currFramePosition, Quaternion.identity);
		}

		if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.A)) { //on left mouse button click, selection
			Debug.Log("Attempting selection!");
			Debug.DrawRay (new Vector3 (currFramePosition.x, currFramePosition.y, -10), Vector3.forward*20, Color.blue,10);
			//dumb way without raycasts
			foreach (Fleets fleet in FleetsList) {
				Debug.Log ("Parsing Fleets");
				if (fleet.localPlayerAuthority) {
					Collider coll = fleet.fleetGo.GetComponent<Collider> ();
					if (coll.bounds.Contains (new Vector3 (currFramePosition.x, currFramePosition.y, coll.transform.position.z))) {
						changeSelection (fleet);
						Debug.Log ("Selected fleet: " + fleet.fleetName);
					} else {
						Debug.Log (new Vector3 (currFramePosition.x, currFramePosition.y, coll.transform.position.z) + " does not match an owned fleet");
					}
				} else {
					Debug.Log ("currently parsed fleet isnt owned by player, skipping");
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
			timeController.CmdSpeedUp(netPlay.playerID);
		}

		if (Input.GetKeyDown(KeyCode.Minus)) { //on left mouse button click
			timeController.CmdSlowDown(netPlay.playerID);
		}

		if (Input.GetKeyDown(KeyCode.Space)) { //on left mouse button click
			timeController.CmdPause(netPlay.playerID);
		}



		//______________________________________________________________________

		//TODO add wasd scrolling

		//grab and save the current mouse position to use as a reference for next frame.
		lastFramePosition = cam.ScreenToWorldPoint( Input.mousePosition );
		lastFramePosition.z = 0;

		selFleetArray = selectedFleets.ToArray (); //only for debugging purposes~


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
