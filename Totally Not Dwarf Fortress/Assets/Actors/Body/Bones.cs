using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bone : MonoBehaviour {

	public enum Gender{
		FEMALE = 0,
		MALE = 1
	};

	public const int FEMALE = 0;
	public const int MALE = 1;

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

	public double strength = .5f; //how resistant to damage.|0-1|  100 = impervious
	public bool broken = false;  //broken but still present
	public bool removed = false; //permanently removed, ie cut off

	public bool useHierarchy = false; //lower down bones will be disabled
	public int hierarchy = 0;
	public bool disabled = false;  //bone will be treated as broken, even if full health. can still recieve damage

	public bool canRegen = true; //the bone will regenerate over time
	public int regenTime = 3600; //time it takes to heal 1 point of health

	public int mHealth= 25;
	public int health =25;




	public Bone(){
		
	}
	public Bone(double s, int h, bool reg, int regT, int mh){
		strength = s;
		hierarchy = h;
		if (h > 0)
		{
			useHierarchy = true;
		}
		canRegen = reg;
		regenTime = regT;
		mHealth = mh;
		health = mHealth;
	}
	public Bone(double s,  bool reg, int regT, int mh){
		strength = s;
		canRegen = reg;
		regenTime = regT;
		mHealth = mh;
		health = mHealth;
	}

	public Bone(double s, int mh){
		strength = s;

		mHealth = mh;
		health = mHealth;
	}

	public Bone(double s, int mh, int h){ //general use
		strength = s;
		mHealth = mh;
		health = mHealth;
		hierarchy = h;
		if (h > 0)
		{
			useHierarchy = true;
		}
	}
	public Bone(double s, int mh, int h, bool reg){ //use for when regen needs to be disabled
		strength = s;
		mHealth = mh;
		health = mHealth;
		hierarchy = h;
		canRegen = reg;
		if (h > 0)
		{
			useHierarchy = true;
		}
	}
//_______________________________________________________Methods

	public void breakBone ( Bone bone){
		bone.health = 0;
		checkBone (bone);
	}

	public void breakAllBones ( List<Bone> bList){
		foreach (Bone b in bList) {
			breakBone (b);
		}

	}

	public void removeBone(Bone bone){
		breakBone (bone);
		bone.canRegen = false;
		bone.removed = true;
	}

	public void checkBone ( Bone bone){
		if (bone.health < 0) {
			bone.health = 0;
		}
		if (bone.health > bone.mHealth) {
			bone.health = bone.mHealth;
			Debug.Log (bone.ToString() + " was above max health, this shouldnt happen");
		}
		if (bone.health == 0) {
			bone.broken = true;
		}

	}


	//__________________________________________________________

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
