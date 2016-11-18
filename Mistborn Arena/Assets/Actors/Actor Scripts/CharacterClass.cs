using UnityEngine;
using System.Collections;

public class CharacterClass : MonoBehaviour {

	//this class contains all stats for actor.  the archetype is set by choosing the enumerator and running the initializer with that input.

	private NetworkView netview;

	public enum Metals //list of metals
	{
		IRON=0, //pull
		STEEL=1,//push
		TIN=2,	//remove fog of war
		PEWTER=3,//strength & speed
		BRONZE=4,//radar
		DURALUMIN=5,//boost next burn
		ATIUM=6 //atium bubble.  Unblockable attacks, 
	};

	float[] Arsenal = new float[7]; //inventory of metals
	float mArsenal = 100; //max of single element in arsenal

	public enum Archetype  //class
	{
		KELSIER, //during the final empire, pre ascension
		VIN,   //Vin during the final empire
		VIN2,  //Vin pre ascension
		INQUISITOR, //during final empire
		WAX
	};
	public Archetype playerArchetype;

	public enum Weapons
	{
		DAGGERS,
		CANE,
		SPEAR,
		VINDICATION,
		STERRION
	};

	public Weapons equippedWeapon;

	public float minRange = 0;
	public float range = 0;       //range in units
	public float attackSpeed = 0; //attacks per second
	public float baseDamage = 0;  //
	public float critChance = 0;  //% chance to crit per attack
	public float bleedChance = 0; //% chance to cause bleed debuff per attack
	
	public string name = "Unintialized";
	public string className = "Unknown";
	public string weaponName = "None";

	public float health = 0;
	public float mHealth = 0;

	public float stamina = 0; //used for ability casts
	public float mStamina = 0;

	public float weight = 0;	//weight
	public float skill = 0;		//attack speed, guard chance etc
	public float strength = 0; //damage per hit
	public float ability = 0;  //damage from abilities

	private bool initialized;


	public bool isInitialized(){
		return initialized;
	}

	// Use this for initialization
	void Start () {
		netview = GetComponent<NetworkView> ();
		Initialize (Archetype.VIN2, Weapons.DAGGERS);//TODO
	}
	
	// Update is called once per frame
	void Update () {
		checkStamina ();
	}

	void checkStamina(){
		if (stamina < mStamina) {
			stamina = stamina + 5*Time.deltaTime;

		}
		if (stamina > mStamina) {
			stamina = mStamina;}
	}


	void Initialize (Archetype arch, Weapons wep){  //sets up player based on chosen class
		initialized = true;
		equippedWeapon = wep;
		playerArchetype = arch;

		switch (playerArchetype) {

		case Archetype.VIN :   
			name = "Vin Tekiel";
			className = "The Street Urchin";
			 
			mHealth = 85f; //baseline 100
			mStamina = 100f; //baseline 100

			weight = 50f; //baseline 75
			skill = 100f; //baseline 100
			strength = 70f;
			ability = 90f;
			break;
		
		case Archetype.VIN2 :
			name = "Vin Venture";
			className = "The Shard of Preservation";

			mHealth = 50f; //baseline 100
			mStamina = 150f; //baseline 100
			
			weight = 60f; //baseline 75
			skill = 110f; //baseline 100
			strength = 80f;
			ability = 120f;



			break;
		case Archetype.KELSIER : 
			name = "Kelsier";
			className = "The Survivor of Hathsin";

			mHealth = 110f; //baseline 100
			mStamina = 75f; //baseline 100
			
			weight = 75f; //baseline 75
			skill = 110f; //baseline 100
			strength = 100f;
			ability = 100f;
			break;

		case Archetype.WAX :
			break;
		case Archetype.INQUISITOR :
			name = "Steel Inquisitor";
			className = "The Harbinger of Ruin";

			mHealth = 150f; //baseline 100
			mStamina = 50f; //baseline 100
			
			weight = 100f; //baseline 75
			skill = 65f; //baseline 100
			strength = 110f;
			ability = 100f;
			break;

		}
		switch (equippedWeapon)
		{
		case Weapons.DAGGERS:
			weaponName = "Obsidian Daggers";
			range = 1f;
			attackSpeed = 4.5f;
			baseDamage = 5f;
			critChance = 2f;
			bleedChance = 5f;
			minRange = 0f;
			break;
		case Weapons.CANE:
			weaponName = "Dueling Cane";
			range = 2f;
			attackSpeed = 2.75f;
			baseDamage = 7.5f;
			critChance = 2f;
			bleedChance = 1f;
			minRange = 0f;
			break;
		case Weapons.SPEAR:
			weaponName = "Obsidian Spear";
			range = 3f;
			attackSpeed = 2.25f;
			baseDamage = 10f;
			critChance = 3.5f;
			bleedChance = 5f;
			minRange = 1f;
			break;
		case Weapons.VINDICATION:
			weaponName = "Vindication Revolver";
			range = 10f;
			attackSpeed = 1.5f;
			baseDamage = 20f;
			critChance = 10f;
			bleedChance = 75f;
			minRange = 2f;
			break;
		case Weapons.STERRION:
			weaponName = "Sterrion Revolvers";
			range = 6.5f;
			attackSpeed = 2f;
			baseDamage = 10f;
			critChance = 2.5f;
			bleedChance = 5f;
			minRange = 1f;
			break;

		}
		health = mHealth;
		stamina = mStamina;
		initialized = true;
		Debug.Log (className + " is initialized!");
	}






}
