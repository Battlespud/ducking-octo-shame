using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	public GameObject circleCursor;

	Vector3 lastFramePosition;

	public int activeLevel = 0;

	//TODO add active level to account for multiple levels and Z component




	Tile GetTileAtWorldCoord(Vector3 coord) {
		int x = Mathf.FloorToInt (coord.x);
		int y = Mathf.FloorToInt (coord.y); 
		int z = activeLevel;

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

		Vector3 currFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		currFramePosition.z = -1;

		// Update the circle cursor position
		circleCursor.transform.position = currFramePosition;

		//get tile udner mouse
		Tile tileUnderMouse = GetTileAtWorldCoord(currFramePosition);
	/*
		if (tileUnderMouse != null) {
			circleCursor.SetActive (true);
			Vector3 cursorPosition = new Vector3 (tileUnderMouse.X, tileUnderMouse.Y, -1);
		} else {
			circleCursor.SetActive (false);
		}
*/


		Vector3 diff;

		// Handle screen dragging
		if( Input.GetMouseButton(1) ) {	// Right

			diff = lastFramePosition - currFramePosition;
			Camera.main.transform.Translate( diff );

		}

		//TODO add wasd

		lastFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		lastFramePosition.z = 0;
	}
}
