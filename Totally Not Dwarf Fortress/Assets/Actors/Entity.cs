using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Entity : MonoBehaviour {


	//Anything that moves.  Parent class. 


	//Instructions for Skeleton and OrganSystem;  
	/*Each individual bone/organ needs many lines.  A bool on whether or not its present, its max health, its current health as floats, and a bool for destroyed or not.  Also a function must be included for
	 * determining its functionality if it isnt a linear relation with health
	 * 
	 * */
	public string entName; //non-unique
	public int ID;     //unique and never reused
	public static int IDMain;  //ALWAYS INCREASE BEFORE USING, IDMAIN SHOULD ALWAYS SHOW THE LAST USED ID NUMBER

	public int Mass; //in KG
	public int Height; //in M

	public void increaseIDMain(){
		IDMain++;
	}

	public enum Gender{
		FEMALE = 0,
		MALE = 1
	};




	public enum Paralysis{
		NONE,
		PARA, //legs dont work
		MINORPARA, //impairment but not total
		TETRA //Stephen Hawking, but can still cry
	};
	


	public int mArms; //how many arms this thing should have
	public int arms; //how many it does
	public float armsHealth; //average health,

	public int mLegs;
	public int legs;
	public float legsHealth;

	public int mWings;
	public int wings;
	public float wingsHealth;






	void Start () {
	
	}
	
	void Update () {
	
	}
}
