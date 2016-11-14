using UnityEngine;
using System.Collections;
using System;

public class Tile {

	public enum TileType {Empty, Floor};

	Action<Tile> cbTileTypeChanged;

	TileType type = TileType.Empty;

	public void RegisterTileTypeChangedCallback(Action<Tile> callback) {
		cbTileTypeChanged += callback;
	}

	public void UnegisterTileTypeChangedCallback(Action<Tile> callback) {
		cbTileTypeChanged -= callback;
	}

	public TileType Type {
		get {
			return type;
		}
		set {
			type = value;
			//update graphics via callback
			cbTileTypeChanged(this);
		}
	}

	LooseObject looseObject;
	InstalledObject installedObject;

	World world;
	int x;

	public int X {
		get {
			return x;
		}
	}

	int y;

	public int Y {
		get {
			return y;
		}
	}

	int z;

	public int Z {
		get {
			return z;
		}
	}

	public Tile (World world, int x, int y, int z){
		this.world = world;
		this.x = x;
		this.y = y;
		this.z = z;
	}









	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
