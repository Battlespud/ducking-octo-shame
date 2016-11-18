using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fleets : MonoBehaviour {

	float x; float y;

	public static void createFleet(Fleet fl){
		
	}

	Fleets(Fleet fl){
		ThisFleet = fl;
	}

	public enum Fleet{ NONE=0, ALPHA, BETA};
	public Fleet ThisFleet;

	public static List<Ships> FleetAlpha;
	public static List<Ships> FleetBeta;
	public static List<Ships> FleetNone;

	public static List<Fleets> FleetList;

	public static void assignFleet (Ships ship,  Fleet fl){
		Fleet formerFleet = ship.assignedFleet;
		switch (formerFleet) { //remove from previous fleet
		case Fleet.ALPHA:
			{
				FleetAlpha.Remove (ship);
				break;
			}
		case Fleet.BETA:
			{
				FleetBeta.Remove (ship);
				break;
			}
		case Fleet.NONE:
		default:
			{
				FleetNone.Remove (ship);
				break;
			}
		}

		switch (fl) { //Assign new fleet and add to list
		case Fleet.ALPHA:
			{
				FleetAlpha.Add (ship);
				break;
			}
		case Fleet.BETA:
			{
				FleetBeta.Add (ship);
				break;
			}
		case Fleet.NONE:
		default:
			{
				FleetNone.Add (ship);
				break;
			}
		}
		ship.assignedFleet = fl;
	}




	// Use this for initialization
	void Start () {
	
	}






	// Update is called once per frame
	void Update () {
	
	}
}
