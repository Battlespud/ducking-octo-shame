using UnityEngine;
using System.Collections;
using System.Collections.Generic;








/*
 * This script should be put in a permanent object and must be running at all times.
 * 
 * The purpose of this script is to store enumerations and other values that will then be set for individual tile objects, as well as to set the overall world stats.  
 * 
 * Dictionary should contain only terraintiles, while TileArray should contain only GameObjects.
 * 
 * 
 * */

	


public class TerrainManager : MonoBehaviour {


	//Terrain  TODO: Add Descriptions


	public Transform TileFab; //load the prefab tile.  Needs to be set in Scene View.  Drag and drop onto script container.
	private TerrainTile terra;
	public int activeLevel =1;

	const int worldsize = 5; //all worlds are square until proven otherwise.  5280 Default
	//WARNING: Actual size is this value squared, times the y limit, so be careful with going too crazy
	const int ylimit = 4;//number of levels * 2.  must be even
	const int ZERO = 0;
	const int TICK = 1;



	//Tile Containers and Methods
	List<GameObject> TileArray = new List<GameObject>(); //used for storing the gameobjects themselves.  Whenever updating the tile, be sure to call tile.updateObject() to avoid a desync.


	//

	//Other Methods
	public static bool CheckPassable (TerrainTile tile){


		var top = tile.topper; //check for toppers or inpassable terrain type.
		var type = tile.terrainType;

		switch (top) {
		case TerrainTileParent.Topper.Tree:
			{
				return false;
			}
		default:
			{
				switch (type) {
				case TerrainTileParent.TerrainType.Empty:
					{
						return true;
					}
				default:
					{
						return false;
					}
				}
			}
		}
	}
	//_______________________________________________________________
	//The Sacred Dictionary, mess with it not\\


	//Create a dictionary at
	Dictionary<TerrainTileParent.Coordinates, TerrainTile> TerrainDictionary = new Dictionary<TerrainTileParent.Coordinates, TerrainTile>(); 


	public void addDic(TerrainTile tile)
	{
		TerrainDictionary.Add (tile.coords, tile);
	}

	public void delDic(TerrainTile tile)
	{
		TerrainDictionary.Remove (tile.coords);
	}


	public void initializeScripts()
	{
	}

	public void initializeDictionary(){
		foreach (var obj in TileArray) {
			TerrainTile t = obj.GetComponent<TerrainTile> ();
			addDic (t);
			t.passable = CheckPassable (t);
		}
	}






	//_______________________________________________________________________
	//The Sacred Worldgenerator, mess with it veril88888888y\\


	//TODO: Make a world generator
	public void GenerateDebugWorld(){
		Debug.Log ("Starting Worldgen");
		int x = 0; int y = 0; int z =0;
		int i = 1;




		while (x < worldsize + 1) { //Generate tiles one line at a time
			Transform t = Instantiate(TileFab,new Vector3(x, y, z), Quaternion.identity) as Transform;
		//	GameObject g = t.gameObject;
			TileArray.Add (t.gameObject);
			x++;
			if (x == worldsize + 1 && z < worldsize) {
				x = ZERO;
				z++;
			}
			if (x == worldsize + 1 && z == worldsize && y > (-1*ylimit)) {
				x = ZERO;
				z = ZERO;
				y-=2;
			}

		//	Debug.Log("Made Tile: " + x + ", " + y + ", " + z + ".  Number: " + i); //comment out before building to run faster
			i++;
		}
		Debug.Log("Tile Array has " + TileArray.Count + " entries!");
		int j = ZERO;
		foreach (GameObject obj in TileArray) {
			j++;
			foreach (var comp in obj.GetComponents<Component>()) { //Debug stuff, remove later
				Debug.Log (comp.ToString());
			}
			terra =  obj.GetComponent<TerrainTile>() ;
			terra.SetTerrainTile (obj.transform.position);
			terra.updateScript ();
			obj.SetActive (false); //set inactive to prevent lag on game start by instaspawning all objects.
		}

		activeLevel = ZERO;		
		initializeDictionary (); //add tilearray to dictionary
		ChangeActiveLevel ();  //set appropriate layer active
	}



	public void ChangeActiveLevel(){				//scans through theblocks and sets all those not appearing on the current level to inactive in order to save performance on very large maps.
		foreach (GameObject obj in TileArray) {
			obj.SetActive(false) ;
			//TODO
			/*
			if (obj.transform.position.y == activeLevel || (activeLevel != 0 && TerrainDictionary[obj.GetComponent<TerrainTile>().coords.y+2].terrainType != TerrainTileParent.TerrainType.Empty)  {
				obj.SetActive(true);
			}

*/

		}
		return;
	}


	//__________________________________________________________________________








	// Use this for initialization
	void Start () {
		GenerateDebugWorld ();
	}
	
	// Update is called once per frame
	void Update () {
		

		if (Input.GetKeyDown (KeyCode.DownArrow)){
		 //print ("down key was pressed");
			if (activeLevel != -1*ylimit) {
				
				activeLevel-=2;
				ChangeActiveLevel ();
			}				
		}
	
		if (Input.GetKeyDown (KeyCode.UpArrow)){
		//print ("up key was pressed");
			if (activeLevel != 0) {
				activeLevel+=2;
				ChangeActiveLevel ();
			}
	}
}
}
