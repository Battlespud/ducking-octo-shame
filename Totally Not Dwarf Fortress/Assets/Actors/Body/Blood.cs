using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bloodsystem : MonoBehaviour {


	//system for simulating blood flow and loss
	public double mBlood = 100; //maxblood, in Liters
	public double cBlood = mBlood; //current blood
	public double pBlood = 1; //blood as a percentage in decimal

	public double regBlood = .02; //regen rate as percentage/hour
	public bool canRegenBlood = true;

	public List<Artery> ArteryList = new List<Artery> ();





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
