using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Planets : MonoBehaviour {

	//This component will be added to every celestial body that we want to have resources
	//or have any real relevance to the game.  For now we'll just have predetermined
	//resources for the major planets, and we'll add a random system for everything else later.

	//A list of every planet in the system, used for easy pathfinding later
	public static List<Planets> PlanetList;

	//contains all the natural resources on this planet
	//build this list in the script made for the individual planet, not here
	public List<NaturalResources> PlanetNaturalResourcesList; 
	public List<Facilities> FacilityList;

	//fetch the gameobject that represents the planet and keep our position here up to date with its coordinates
	Vector3 position;
	GameObject planetGo;
	public GameObject planetOrbitGo;
	OrbitAlphaController orbitAlphaController;
	bool orbitSelected = false;

	 void selectOrbit(){
		orbitSelected = true;
		orbitAlphaController.VisiblePercent = orbitAlphaController.selectedVisibility;
	}

	 void unselectOrbit(){
		orbitSelected = false;
		orbitAlphaController.VisiblePercent = orbitAlphaController.regularVisibility;
	}

	public void checkOrbitSelection(){
		Debug.Log ("Planet selection has changed!");
		switch (orbitSelected) {
		case true:
			unselectOrbit ();
			break;
		case false:
			selectOrbit ();
			break;
		}
	}

	//we'll set this from the script relating to the specific planet, not here
	public string Name;

	//Returns a string listing amount available of each resource
	public string getResourceString(){ 
		string resourceString = System.String.Empty; //initialize to empty
		foreach (NaturalResources r in PlanetNaturalResourcesList) {
			resourceString += r.type.ToString() +": " + r.amountAvailable + "; ";
		}
		return(resourceString);
	}

	void getPlanetOrbitGo(){
		foreach (Transform child in transform.parent) {
			if (child.name != this.name) {
				planetOrbitGo = child.gameObject; 
				orbitAlphaController = planetOrbitGo.GetComponent<OrbitAlphaController> ();
			}
		}
	}



	// Use this for initialization
	void Start () {
		planetGo = this.gameObject;
		position = planetGo.transform.position;
		getPlanetOrbitGo ();
	}
	
	// Update is called once per frame
	void Update () {
		position = planetGo.transform.position;

	}
}
