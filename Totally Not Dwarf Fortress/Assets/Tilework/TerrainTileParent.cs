using UnityEngine;
using System.Collections;
using System;

public class TerrainTileParent : MonoBehaviour {

	//map size should be runnable at 5280*5280 = 1 sq mile.


	public enum TerrainType{
		Empty=0, //Nothing is in this slot, tile below will be displayed instead.
		/*Non-Resource terrain*/		Stone, Dirt, Grass, Farmland, Clay, 
		/*Water*/						Lake, Ocean, RiverN, RiverS, RiverE, RiverW, //River cardinal direction is the direction the water flows TOWARDS.  Lake and Ocean are stillwater, ocean is darker and impassable.
		/*Metals*/						Iron, Copper, Gold, Silver, Coal, Obsidian

	};

	public TerrainType terrainType;


	public bool passable;  //TODO: Make container for passable unpassable enum values to simplify sorting.

	//Toppers refer to any object that sits on the tile, only one per tile.

	public enum Topper{
		Empty=0,
		Grass,
		Rubble,
		Road,
		Floor,
		Tree //placeholder, TODO: add more types

	};

	public Topper topper;

	public struct Coordinates{
		public int x;public int y;public int z;
		public Vector3 vec;

		public  Coordinates(Vector3 v){
			 x = (int) v.x;
			 y = (int) v.y;
			 z = (int) v.z;
			vec = v;
			if ( y > 1) { throw new System.IndexOutOfRangeException();} //no mountains, not yet
		}
		public  Coordinates(int a, int b, int c){
			x = a;
			y = b;
			z = c;
			vec = new Vector3(a, b, c);
			if ( y > 1) { throw new System.IndexOutOfRangeException();} //no mountains, not yet
		}

	}

	public Coordinates coords;















	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
