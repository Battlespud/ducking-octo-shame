using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	PlayerControls plyrCtrl = new PlayerControls (Input.GetKey());
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		plyrCtrl.movePlayer ();
		/*
		//x,y,z  x is left and right, z is up and down, y is vertical
		//up N
		if(Input.GetKey(N)){
			transform.Translate (0,0,1 * mS * Time.deltaTime);
		}
		//up right NE
		if(Input.GetKey(N) && Input.GetKey(E)){
			transform.Translate (1 * (mS * Time.deltaTime)/4, 0, 1 * (mS * Time.deltaTime)/4);
		}
		//right E
		if(Input.GetKey(E)){
			transform.Translate (1 * mS * Time.deltaTime, 0, 0);
		}
		//right down SE
		if(Input.GetKey(S) && Input.GetKey(E)){
			transform.Translate (1 * (mS * Time.deltaTime)/4, 0, -1 * (mS * Time.deltaTime)/4);
		}
		//down S
		if(Input.GetKey(S)){
			transform.Translate (0, 0, -1 * mS * Time.deltaTime);
		}
		//down left SW
		if(Input.GetKey(S) && Input.GetKey(W)){
			transform.Translate (-1 * (mS * Time.deltaTime)/4, 0, -1 * (mS * Time.deltaTime)/4);
		}
		//left W
		if(Input.GetKey(W)){
			transform.Translate (-1 * mS * Time.deltaTime, 0, 0);
		}
		//left up NW
		if (Input.GetKey (N) && Input.GetKey (W)) {
			transform.Translate (-1 * (mS * Time.deltaTime)/4, 0, 1 * (mS * Time.deltaTime)/4);
		} 
		//inc spd
		if (Input.GetKey (KeyCode.A)) {
			mS++;
			Debug.Log (mS);
		}
		//dec spd
		if (Input.GetKey (KeyCode.D)) {
			mS--;
			Debug.Log (mS);
		}
		//limits speed to 0
		if (mS <= 0) {
			mS = 1;
		}
		//limits speed to 20
		if (mS > 20) {
			mS = 20;
			Debug.Log ("Move speed will not exceed 20");
		}
		*/
		}
	}
