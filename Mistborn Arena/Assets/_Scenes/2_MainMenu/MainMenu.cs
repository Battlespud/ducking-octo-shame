using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnGUI()
	{
			if (GUI.Button (new Rect (100, 100, 250, 100), "Single Player")) {
//TODO
			Debug.Log("SinglePlayer");
		}
			
			if (GUI.Button (new Rect (100, 250, 250, 100), "Multiplayer")) {
			Debug.Log ("Multiplayer");
			Application.LoadLevel ("a_ServerBrowser");
		}
	}




}
