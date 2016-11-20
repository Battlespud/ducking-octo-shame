using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Planets : MonoBehaviour {


	public static List<Planets> PlanetList;

	Vector3 position;
	GameObject planetGo;

	public string Name;

	// Use this for initialization
	void Start () {
		planetGo = this.gameObject;
		position = planetGo.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		planetGo.transform.Rotate (0, 0, 2 * Time.deltaTime);
	}
}
