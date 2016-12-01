using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OrbitAlphaController : MonoBehaviour {

	//All this does is make the orbital path transparent

	//Selection will 
	SpriteRenderer orbitPathSpriteRenderer;

	//Gameobject of the planet this path is for.
	GameObject planetGo;
	Collider coll;

	public float regularVisibility = .05f;
	public float selectedVisibility = .85f;
	//set equal to either of the above to change
	float visiblePercent = .05f;

	public float VisiblePercent {
		get {
			return visiblePercent;
		}
		set {
			visiblePercent = value;
			updateAlpha ();
		}
	}

	void updateAlpha(){
		orbitPathSpriteRenderer.color = new Color(1f, 1f, 1f, visiblePercent);
	}

	// Use this for initialization
	void Start () {
		orbitPathSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		//Last value is alpha value
		orbitPathSpriteRenderer.color = new Color(1f, 1f, 1f, visiblePercent);
		getPlanetGo ();
	}

	//Function will only work if no other gameobjects are put as siblings!!!
	void getPlanetGo(){
		foreach (Transform child in transform.parent) {
			if (child.name != this.name) {
				planetGo = child.gameObject; 
				coll = planetGo.GetComponent<SphereCollider> ();
			}
		}
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
