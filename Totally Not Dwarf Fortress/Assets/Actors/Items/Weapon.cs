using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	//Name

	public string Material; //bronze
	public string Name; //shovel
	public string Descriptive; //of digging
	public string condensedName;



	public int mCD = 60; // 60*sec
	public int CD = 0; 
	public bool onCD = false;



	public Weapon(){
		Material = "Broken";
		Name = "Sword";
		Descriptive = "Debugging";
		buildName (this);
		avgDamage = 10000;

	}


	void checkCD()
	{
		if (CD != 0) {
			onCD = true;
			CD--;
		} else {
			onCD = false;
		}
	}

	public static void buildName(Weapon wep){
		wep.condensedName = wep.Material + " " + wep.Name + " of " + wep.Descriptive;
		return;
	}

	//Ints_____________________________________________________________________________________

	public int wepID;
	public static int wepIDMain; //always increase before using

	public int mDamage; //max damage
	public int avgDamage; //average to calculate from

	public float armorPen=.5f; //what percent of armor to skip

	public int Mass = 2; //in KG
	public int Length = 1;//meters  

	public int Position=6;  //two handers will be treated as 8 position for humans


	//Enums___________________________________________________________________________________
	public enum WeaponType{
		NONE,
		CLUB,
		SWORD,
		KNIFE,
		SPEAR,
		JAVELIN,
		BOW,
		ARROW,
		RIFLE,
		PISTOL
	}

	public enum Rarity{
		COMMON,
		UNCOMMON,
		RARE,
		LEGENDARY,
		MYTHIC
	};
	public Rarity rarity = Rarity.COMMON;

	public enum DamageType{ //what type of damage to deal
		RIP,
		BLOW,
		CUT,
		SHOT,
		BURN,
		SHATTER,
		SMASH,
		CRUSH
	};
	public DamageType damageType;


	public enum RangeType{
		MELEE,
		PROJECTILE
	};
	public RangeType rangeType;

	//Boolean__________________________________________________________________________________

	public bool isBlunt = false; //blunt will  be more likely to knock unconcious
	public bool useAmmo = false;
	public bool twoHanded = false;
	public bool inspiresFear = false;












	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		checkCD ();
	}
}
