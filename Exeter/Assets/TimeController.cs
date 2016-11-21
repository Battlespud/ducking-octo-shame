using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TimeController : NetworkBehaviour {


	//Timescales
	[SyncVar]public float targetTimeScale = 1f;
	[SyncVar]public bool paused = false;

	const float timeScaleMax = 5f;
	const float timeScaleMin = .25f;

	[Command]
	public void CmdPause (string playerID){
		if (paused) {
			paused = false;
			Debug.Log ("Unpaused by " + playerID);
		} else {
			paused = true;
			Debug.Log ("Paused by " + playerID);
		}
	}



	[Command]
	public void CmdSlowDown(string id){
		targetTimeScale -= .25f;
		Debug.Log ("Slowed to " + Time.timeScale + " by " + id);
	}

	[Command]
	public void CmdSpeedUp(string id){
		targetTimeScale += .25f;
		Debug.Log ("Sped up to " + Time.timeScale + " by " + id);
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(targetTimeScale >= timeScaleMax) { targetTimeScale = timeScaleMax;}
		if (targetTimeScale <= timeScaleMin) {targetTimeScale = timeScaleMin;	}

		if (paused) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = targetTimeScale;
		}
	}
}
