using UnityEngine;
using System.Collections;

public class World  {

	int activeLevel = 0;

	public void changeLevel(int activeLevl){
		this.activeLevel = activeLevel;
		foreach (Tile t in tiles) {
			//TODO find a way to get Tile_go
		}
	}

	Tile[,,] tiles;
	int x;

	public int X {
		get {
			return x;
		}
	}

	int z;

	public int Z {
		get {
			return z;
		}
	}

	int y;

	public int Y {
		get {
			return y;
		}
	}


	public  void RandomizeTiles(){
		for (int x = 0; x < X; x++) {
			for (int y = 0; y < Y; y++) {
				for (int z = 0; z < Z; z++) {
					if (tiles [x, y, z] != null) {
						int c = Random.Range (1, 3);
						if (c==1) {
							tiles [x, y, z].Type = Tile.TileType.Empty; 
							}
						if (c==2){
							tiles [x, y, z].Type = Tile.TileType.Floor; 
						} 
						}
						}
					}
				}
		Debug.Log ("Randomized!");
			}



	public void RandomizeTile(Tile t){
		if (Random.Range (0, 2) == 0) {
			t.Type = Tile.TileType.Empty; 
		}
	 else {
		t.Type = Tile.TileType.Floor; 
		} 
	}
		


	public Tile GetTileAt(int x, int y, int z) {
		return tiles[x, y, z];
	}


	public World(int x = 50, int y = 50, int z = 2) { //default sizes, can be overwritten tho
		this.x = x;
		this.z = z;
		this.y = y;

		tiles = new Tile[x, y, z];

		for (int xx = 0; xx < x; xx++) {
			for (int yy = 0; yy < y; yy++) {
				for (int zz = 0; zz < z; zz++){
				tiles[xx,yy, zz] = new Tile(this, xx, yy, zz);
			}
		}
	}

		Debug.Log ("World created with " + (x*z*y) + " tiles. " + tiles.Length);
	}



}
