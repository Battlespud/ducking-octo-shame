using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Combat : MonoBehaviour {

	//idk even how to make this work so yolo fam






	static void attack(Human attacker, Human defender)
	{
		//damage will be dealt to limb or body parts at selected value
		var targetPos = chooseTargetPosition(attacker, defender);









	}

	static int chooseTargetPosition(Human attacker, Human defender){
		attacker.gameObject.transform.LookAt (defender.gameObject.transform);
		int relativeTargetPosition; //using keypad notation
		List<int>PossiblePositions;
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
		Vector3 attackVector = new Vector3 (); //used to determine how far the attacker is, not strictly neccessary, consider replacing references with hitvector
		//get attacker direction
		attackVector = attacker.gameObject.transform.InverseTransformPoint(defender.gameObject.transform);
		hitVector = defender.gameObject.transform.InverseTransformPoint(attacker.gameObject.transform);

		if ((attackVector.x < -1 || attackVector.x > 1 || attackVector.y != 0 || attackVector.z < -1 || attackVector.z > 1)) {
			Debug.Log ("Attempted attack over invalid distance");
			return;
		}

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
