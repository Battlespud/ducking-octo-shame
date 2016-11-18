using UnityEngine;
using System.Collections;

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
