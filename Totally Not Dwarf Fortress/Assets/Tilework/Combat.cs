using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Combat : MonoBehaviour {

	//idk even how to make this work so yolo fam


	//_______________________________________________________________________________________________



	static void attack(Human attacker, Human defender)
	{
		if (attacker.weapon.onCD) {
			return;   //weapon isnt ready to attack again  yet
		}
		//damage will be dealt to limb or body parts at selected value
		int targetPos = chooseTargetPosition(attacker, defender);
		//Roll for hit
		if (!checkHit (attacker, defender)) {
			return; //attack misses
		}
		deliverDamage (attacker, defender,targetPos);

	}


	//________________________________________________________________________________________________



	static void deliverDamage(Human attacker, Human defender, int targetPos){
		Bone b = new Bone();
		Arm a = new Arm (6, 1);
		bool severed = false;
		if (targetPos == 4) {
			a = defender.LeftArm;
			b = defender.LeftArm.ArmBones[Random.Range(1,defender.LeftArm.ArmBones.Count)];
		}
		if (targetPos == 6) {
			a = defender.RightArm;
			b = defender.RightArm.ArmBones[Random.Range(1,defender.RightArm.ArmBones.Count)];
		}
		if (checkSever (attacker.weapon, b)) { //remove arm if severed
			foreach(Bone c in a.ArmBones){
				if (b.hierarchy < c.hierarchy && c.useHierarchy) {
					c.removeBone(c);
				}
				if (defender.weapon != null && a == defender.RightArm) {
					defender.weapon = null;
				}
			}
			severed = true;
		}



		try{
			Debug.Log(a +" has been hit! Severed: " + severed);
		b.health -= attacker.weapon.avgDamage;
		b.checkBone (b);
		}
		catch {//bone wasnt set successfully probably
		}
}


	static bool checkSever(Weapon w, Bone b){
		return(w.avgDamage > b.health * 1.5);
	}
		

	static bool checkHit(Human attacker, Human defender){
		//TODO
		return true;

	}



	static int chooseTargetPosition(Human attacker, Human defender){
		attacker.gameObject.transform.LookAt (defender.gameObject.transform);
		int relativeTargetPosition; //using keypad notation
		List<int>PossiblePositions = new List<int>();
		int i = 0;
		while (i < 9) {
			i++;
			PossiblePositions.Add (i);
		}
		/* z > 0 = target in Front, z < 0 = target Behind.
		 * x > 0 = target is Right, x < 0 = target Left
		 * 
		 * */
		Vector3 hitVector = new Vector3 (); //used to determine which part of the defender was hit
		//get attacker direction
		hitVector = defender.gameObject.transform.InverseTransformPoint(attacker.gameObject.transform.position);



		//determine direction of hit
		if (hitVector.x < 0) {
			try{
				PossiblePositions.Remove(9);
			}
			catch{}
			try{
				PossiblePositions.Remove(6);
			}
			catch{}
			try{
				PossiblePositions.Remove(3);
			}
			catch{}
		}

		if (hitVector.x > 0) {
			try{
				PossiblePositions.Remove(7);
			}
			catch{}
			try{
				PossiblePositions.Remove(4);
			}
			catch{}
			try{
				PossiblePositions.Remove(1);
			}
			catch{}
		}


		if (hitVector.z < 0) {
			try{
				PossiblePositions.Remove(7);
			}
			catch{}
			try{
				PossiblePositions.Remove(8);
			}
			catch{}
			try{
				PossiblePositions.Remove(9);
			}
			catch{}
		}	
		if (hitVector.z > 0) {
			try{
				PossiblePositions.Remove(1);
			}
			catch{}
			try{
				PossiblePositions.Remove(2);
			}
			catch{}
			try{
				PossiblePositions.Remove(3);
			}
			catch{}
		}
		int selectedValue;
		do{
			selectedValue = Random.Range(0,10);
		} while (!PossiblePositions.Contains(selectedValue));
		return selectedValue;
	}




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
