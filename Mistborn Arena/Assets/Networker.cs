using UnityEngine;
using System.Collections;

public class Networker : MonoBehaviour {

	NetworkView netView;


	// Use this for initialization
	void Start () {
		netView = GetComponent<NetworkView> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onSerializeNetworkView( BitStream stream, NetworkMessageInfo info){


	}
}
