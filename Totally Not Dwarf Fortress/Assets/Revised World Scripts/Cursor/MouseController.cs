using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	public GameObject circleCursor;

	Vector3 lastFramePosition;

	public int activeLevel = 0;

	//TODO add active level to account for multiple levels and Z component


	//these are used to control camera zoom levels
	const int perspZoomInLimit = 5; //minimum distance from plane
	const int perspZoomOutLimit = 50; //max distance from plane
	const int perspZoomSpeed = 15;

	const int orthoZoomInLimit = 5; //smallest size
	const int orthoZoomOutLimit = 45; //max size of orthographic view, bigger is much more intensive
	const int orthoZoomSpeed = 10; //multiplier for mouse input



	Tile GetTileAtWorldCoord(Vector3 coord) {
		int x = Mathf.FloorToInt (coord.x); //round so that we hit the exact match with tile coordinates, which are always whole numbers
		int y = Mathf.FloorToInt (coord.y); 
		int z = activeLevel; //only poll the current level

		try{
			Debug.Log("Tile at " + x + " " + y + " " + z +  " Type: " +  WorldGenerator._instance.GameWorld.GetTileAt (x, y, activeLevel).Type);
			return WorldGenerator._instance.GameWorld.GetTileAt (x, y, activeLevel);
		}
		catch{
			Debug.Log ("no tile here!"); 
			return null;
		}
	}







	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 currFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );  //poll where the mouse is at the start of each frame and store it
		currFramePosition.z = -1; //always keep the mouse 1 layer closer to the camera than our highest layer in order to keep it visible.

		// Update the circle cursor position
		circleCursor.transform.position = currFramePosition;

		//get tile udner mouse
		Tile tileUnderMouse = GetTileAtWorldCoord(currFramePosition); 
	
		if (tileUnderMouse != null) { //check for if we went off the map
			circleCursor.SetActive (true);
			Vector3 cursorPosition = new Vector3 (tileUnderMouse.X, tileUnderMouse.Y, -1);
		} else {
			circleCursor.SetActive (false);
		}



		Vector3 diff = new Vector3(0,0,0);  //storage container for the difference between where we were and where we moved the mouse to.

		// Handle screen dragging
		if( Input.GetMouseButton(1) ) {	// Right

			diff = lastFramePosition - currFramePosition;  //determine difference in each vector component
			diff.z = 0; //prevent dragging along the z layer by accident.  Prevents major glitches, do not alter
			Camera.main.transform.Translate( diff ); //move the camera the same amount our mouse moved
		}

		if( Input.GetAxis ("Mouse ScrollWheel") != 0f ) {	// zoom function.  Works, but requires a switch to perspective camera and changes to other parts of the script to compoensate.
			switch (Camera.main.orthographic) {
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




		}


		//TODO add wasd

		lastFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		lastFramePosition.z = 0;
	}
}
