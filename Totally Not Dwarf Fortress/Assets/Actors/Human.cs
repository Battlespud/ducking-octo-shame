using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Human : Sentient {

	public List<Bone> Skeleton = new List<Bone>();

	public Weapon[] Weapons = new Weapon[2]; //Can hold 1 weapon in each hand

	public const int MALE = 1;
	public const int FEMALE = 0;

	//How body part positioning is simulated.  Picture a numpad
	public const int FORLEFT = 7;
	public const int FORRIGHT = 9;
	public const int FORMID = 8;

	public const int MIDLEFT = 4;
	public const int MIDRIGHT =6;
	public const int MIDMID = 5;

	public const int REARLEFT = 1;
	public const int REARMID = 2;
	public const int REARRIGHT = 3;

	public Gender gender; 


	public Human(Gender g, string n){
		name = n;
		gender = g; 
		increaseIDMain ();
		ID = IDMain;

		Arm LeftArm = new Arm(MIDLEFT,g); //left and right arms with gender weighting
		Arm RightArm = new Arm(MIDRIGHT,g);

		Leg LeftLeg = new Leg (MIDMID, g);
		Leg RightLeg = new Leg (MIDMID, g);

		Skeleton.Add (LeftArm);
		Skeleton.Add (LeftLeg);
		Skeleton.Add (RightArm);
		Skeleton.Add (RightLeg);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
