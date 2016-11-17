using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	public float mS = 1f;
	// Update is called once per frame
	void Update () {
		
		//x,y,z  x is left and right, z is up and down, y is vertical
		//up N
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.Translate (0,0,1 * mS * Time.deltaTime);
		}
		//up right NE
		if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow)){
			transform.Translate (1 * (mS * Time.deltaTime)/4, 0, 1 * (mS * Time.deltaTime)/4);
		}
		//right E
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.Translate (1 * mS * Time.deltaTime, 0, 0);
		}
		//right down SE
		if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow)){
			transform.Translate (1 * (mS * Time.deltaTime)/4, 0, -1 * (mS * Time.deltaTime)/4);
		}
		//down S
		if(Input.GetKey(KeyCode.DownArrow)){
			transform.Translate (0, 0, -1 * mS * Time.deltaTime);
		}
		//down left SW
		if(Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow)){
			transform.Translate (-1 * (mS * Time.deltaTime)/4, 0, -1 * (mS * Time.deltaTime)/4);
		}
		//left W
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.Translate (-1 * mS * Time.deltaTime, 0, 0);
		}
		//left up NW
		if (Input.GetKey (KeyCode.LeftArrow) && Input.GetKey (KeyCode.UpArrow)) {
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
		}
	}
