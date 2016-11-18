using UnityEngine;
using System.Collections;

/*
public class PlayerControls {



	KeyCode K;




	public PlayerControls(KeyCode k){
		K = k;
	}
	Vector3 move;

	float mS = 1f;

	public void movePlayer(){
		if (K == KeyCode.UpArrow) {
			move += new Vector3 (0, 0, 1) * mS * Time.deltaTime;
		}



	}
}
*/

public static class PlayerControls{

	private const int forward = 1;
	private const int backward = -1;

	public static Vector3 movePlayer(float mSpeed)
	{
		float t = Time.deltaTime;
		Vector3 diff = new Vector3(0,0,0);

		if (Input.GetKey(KeyCode.W)) {
			diff += new Vector3 (mSpeed * t*forward, 0, 0);
			Debug.Log ("Forward");
		}
		if (Input.GetKey(KeyCode.S)) {
			diff += new Vector3 (mSpeed * t*backward, 0, 0);
			Debug.Log ("Backward");
		}
		return diff;
	}

}