using UnityEngine;
using System.Collections;

public class Spine : Bone {


	public bool canRegen = false; //will this heal on its own, default no. Enable with bionic spine or some animals
	public float regenrate = 0;
	public bool severed = false; 

	public int stemHealth; //brainstem, break leads to instant death
	public int mstemHealth;
	//TODO











	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
