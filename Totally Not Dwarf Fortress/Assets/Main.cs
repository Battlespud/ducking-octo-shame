using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	//this is pretty much just for testing spawning behavior etc
	public Transform Char;
	public List<GameObject> ActorList = new List<GameObject> ();





	public void addDebugActor()
	{
		Transform t = Instantiate (Char, new Vector3 (0, .5, 0), Quaternion.identity) as Transform;
		ActorList.Add (t.gameObject);



		return;
	}


	// Use this for initialization
	void Start () {
		addDebugActor ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
