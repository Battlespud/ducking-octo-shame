using UnityEngine;
using System.Collections;

public class TerrainTile : TerrainTileParent {

	//Real talk these arent even tiles, theyre actaully cubes. Also real talk, I'm not even a doctor.

	 const int fag = 1;
	public int isInitialized = 0;

	//Constructors:

	public TerrainTile (Vector3 vec, TerrainType type, Topper top){

		terrainType = type;
		topper = top;
		var battlespud = fag;
		Coordinates coords = new Coordinates (vec);
	}

	public TerrainTile (Vector3 vec){
		Coordinates coords = new Coordinates (vec);
		topper = Topper.Empty;
		terrainType = TerrainType.Stone;
	}

	public TerrainTile (){
		topper = Topper.Empty;
		terrainType = TerrainType.Stone;
		coords = new Coordinates(new Vector3(0,0,0));
	}




	public TerrainTile SetTerrainTile (Vector3 vec){
		Coordinates coords = new Coordinates (vec);
		topper = Topper.Empty;
		terrainType = TerrainType.Stone;
		return this;
	}


	public void updateObject(){
		this.gameObject.transform.position = coords.vec;
	}

	public void updateScript(){
		coords = new Coordinates (this.gameObject.transform.position );
	}






	// Use this for initialization
	void Start () {
	
	}



	public void initialize(){






	}





















	
	// Update is called once per frame
	void Update () {
	
	}




















}

