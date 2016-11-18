using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	private bool levelSelected;
	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
		levelSelected = false;
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnGUI()
	{
		if (!levelSelected) {
			if (GUI.Button (new Rect (100, 100, 250, 100), "Arena")) {
				//TODO
				Debug.Log ("Arena selected");
				levelSelected = true;
				Application.LoadLevelAdditive ("Arena");
				SpawnPlayer();
			}
		
			if (GUI.Button (new Rect (100, 250, 250, 100), "Some Other Level")) {
				//		Debug.Log ("Multiplayer");
				//		Application.LoadLevel ("a_ServerBrowser");
			}
		}

	}

	void SpawnPlayer(){
		Network.Instantiate (playerPrefab, new Vector3 (0, 5, 0), Quaternion.identity, 0);
	}

	void OnPlayerJoin(){
		SpawnPlayer ();
	}
}
