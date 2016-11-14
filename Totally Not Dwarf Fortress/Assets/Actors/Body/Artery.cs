using UnityEngine;
using System.Collections;

public class Artery : Bloodsystem {


	int hierarchy = 0;
	int health = 10;
	int mhealth = 10;
	int regenRate = 1; //rate the artery will heal at/reduce bleeding at

	public bool isBleeding =false;
	public double pBleeding = 0;
	public int mBloodflow;
	public int bloodflow; //bloodflow in liters/hour/5
	public bool damaged = false;

	public void severArtery(Artery a){
		a.bloodflow = 0;

	}

	public void setBleed(Artery a, double pct)
	{
		a.isBleeding = true;
		a.pBleeding = pct;
	}


	public Artery(int h, int bflow){
		mBloodflow = bflow;
		bloodflow = mBloodflow;
		hierarchy = h;
			health = mhealth;
	}










	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
