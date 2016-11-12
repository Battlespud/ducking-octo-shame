using UnityEngine;
using System.Collections.Generic;

public class AIPackage : MonoBehaviour {


	//Actual routines will be in this class.  To apply them to a character,
	// you should add an appropriately built8 INTEL struct.


	Dictionary<Entity, AIPackage> TerrainDictionary = new Dictionary<Entity, AIPackage>(); 


	public enum Faction{ //factions used for determining friends, enemies etc
		/// <summary>
		/// So tbh this is 100% a stupid fucking way of doing this, but its also the easiest to work with.  Enums are built as structs, so you cant add them dynamically after compilation
		/// This means that ALL factions must be created ahead of time, Skyrim style.  This means we're going to need ones for every possible division (ie, cat, dog, bear instead of just animal
		/// </summary>
		NONE=0, //placeholder
		PLAYER = 1,
		REBEL = 2, //hostile all
		CIV1,  //AI civs, can be hostile or friendly
		CIV2,
		CIV3,
		CIV4,
		CIV5,

		//animals
		PREY,
		PREDATOR,
		SUPERPREDATOR

	};

	public enum MentalState{  //possible mental states, generally not editable by player.  Should always be negative or uncontrollable etc.  Refers to mental state, NOT goals.  Ie. STEALING isnt a mental state, its a goal.  KLEPTOMANIAC is an attribute and INSANE is a mental state
		NORMAL=0,  //default option
		PANIC, //move randomly and unproductively away from enemies
		BERSERK, //attack nearby
		INSANE, //execute possibly criminal goal, uncontrollable, no LOS
		CATATONIC, //locked in place, cant take any action
		SUICIDAL, //jump off cliff or into lava or water
		BILLCLINTON,   //find lone female, do things
		ADDICT
	};

	public enum Goal{
		NONE=0,
		FOOD,
		DRINK,
		RUNAWAY,
		FIGHT,
		HIDE,
		OPENDOOR,
		CLOSEDOOR,
		MOVETO,
		SLEEP,
	};




	public float aggression; //how aggressive the entity should be.
	public const float minAg = 0;     //bounds
	public const float maxAg = 100;

	public bool wander = false; //path randomly.  Set true when doing nothing else.


	//initialize enums in case something goes wrong.
	public MentalState mental = 0;  
	public Faction fac = Faction.NONE;
	public Goal goal = Goal.NONE;






















	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}






}
