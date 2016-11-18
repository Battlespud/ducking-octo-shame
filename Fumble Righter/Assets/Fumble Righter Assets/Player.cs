using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	//PlayerControls plyrCtrl = new PlayerControls (Input.GetKey());
	// Use this for initialization
	void Start () {
		
	}
	void movePlayer(){
		transform.Translate(PlayerControls.movePlayer ());
	}
		
	// Update is called once per frame
	void Update () {
		movePlayer ();
		}
	}
