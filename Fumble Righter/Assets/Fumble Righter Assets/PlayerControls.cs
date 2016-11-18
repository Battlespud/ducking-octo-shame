using UnityEngine;
using System.Collections;

public static class PlayerControls{
	//vector3(x,y,z) x is right, -x is left, z is forward, -z is backward, y is up, -y is down
	/*
	 * 0 0 1 N
	 * 1 0 1 NE
	 * 1 0 0 E
	 * 1 0-1 SE
	 * 0 0-1 S
	 *-1 0-1 SW
	 *-1 0 0 W
	 *-1 0 1 NW
	 */
	private const float posXZ = 1;
	private const float negXZ = -1;
	public static Vector3 movePlayer()
	{
		Vector3 plyrMvmnt = new Vector3(0,0,0);
		//x forward N
		if(Input.GetKey(KeyCode.UpArrow)){
			plyrMvmnt += new Vector3 (0, 0, posXZ);
			Debug.Log ("Moving N at " + posXZ + " units.");
		}
		//xz forward/right NE
		if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow)){
			plyrMvmnt += new Vector3 (posXZ, 0, posXZ)/2;
			Debug.Log ("Moving NE at " + posXZ + " units.");
		}
		//z moving right E
		if(Input.GetKey(KeyCode.RightArrow)){
			plyrMvmnt += new Vector3 (posXZ, 0, 0);
			Debug.Log ("Moving E at " + posXZ + " units.");
		}
		//-xz moving backward/right SE
		if(Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)){
			plyrMvmnt += new Vector3 (posXZ, 0, negXZ)/2;
			Debug.Log ("Moving SE at " + posXZ + " units.");
		}
		//-x moving backward S
		if(Input.GetKey(KeyCode.DownArrow)){
			plyrMvmnt += new Vector3 (0, 0, negXZ);
			Debug.Log ("Moving S at " + negXZ + " units.");
		}
		//-x-z moving backward/left SW
		if(Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow)){
			plyrMvmnt += new Vector3 (negXZ, 0, negXZ)/2;
			Debug.Log ("Moving SW at " + negXZ + " units.");
		}
		//-z moving left W
		if(Input.GetKey(KeyCode.LeftArrow)){
			plyrMvmnt += new Vector3 (negXZ, 0, 0);
			Debug.Log ("Moving W at " + negXZ + " units.");
		}
		//x-z moving forward/left NW
		if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.LeftArrow)) {
			plyrMvmnt += new Vector3 (negXZ, 0, posXZ) / 2;
			Debug.Log ("Moving NW at " + posXZ + " units.");
		} else {
			Debug.Log ("Not moving.");
		}

		return plyrMvmnt * Time.deltaTime;
	}

}