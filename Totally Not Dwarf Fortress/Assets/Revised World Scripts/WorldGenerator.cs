using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour {


	//remove later
	public Sprite emptySprite;
	public Sprite grassSprite;

	public static WorldGenerator _instance {get; protected set;}

	public World GameWorld { get; protected set; }






	// Use this for initialization
	void Start () {
		GameWorld = new World ();
		_instance = this;

		//Create a Gameobject for each tile in order to display on screen;

		for (int x = 0; x < GameWorld.X; x++) {
			for (int y = 0; y < GameWorld.Y; y++) {
				for (int z = 0; z < GameWorld.Z; z++) {

					Tile tile_data = GameWorld.GetTileAt (x, y, z); //get Tile data

					GameObject tile_go = new GameObject (); //setup gameobject for scene
					tile_go.name = "Tile_" + x + "_" + y + "_" + z; //name it
					tile_go.transform.position = new Vector3 (tile_data.X, tile_data.Y, tile_data.Z); //make it match data
					tile_go.transform.SetParent(this.transform, true);

					tile_go.AddComponent<SpriteRenderer> (); //add a sprite renderer to be setup later

					//lambda to wrap callback function
					tile_data.RegisterTileTypeChangedCallback ( (tile) => {OnTileTypeChanged(tile,tile_go);});

				}
			}
		}
		//for testing

		Invoke ("doRandomize", 2f); //Waits 2 seconds then randomizes tiles and updates textures

	}




	//should be called whenever a tile type gets changed.  updates the sprite renderer.
	void OnTileTypeChanged(Tile tile_data, GameObject tile_go){
		
		if (tile_data.Type == Tile.TileType.Empty) {
			tile_go.GetComponent<SpriteRenderer> ().sprite = emptySprite;
		}

		if (tile_data.Type == Tile.TileType.Floor) {
			tile_go.GetComponent<SpriteRenderer> ().sprite = grassSprite;
		}

		else 
			tile_go.GetComponent<SpriteRenderer> ().sprite = emptySprite;

	}




	void doRandomize(){
		GameWorld.RandomizeTiles ();
	}



	
	// Update is called once per frame
	void Update () {




	}
}
