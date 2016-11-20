using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Fleets : NetworkBehaviour {


	/*All movement and actions are performed by fleets rather than individual ships.  Though a fleet can consist of a single ship of course.
	Behaviors:
	Fleets move at the speed of their slowest member
	They reduce speed instantly on taking damage or remove lagging ships depending on settings */




	//Statics_________________________________________________________
	List<Fleets> FleetsList;

	public static Sprite fleetSprite;

	Vector3 minRange = new Vector3 (-29.4f,-31.8f,0f) - new Vector3 (-10.3f, -31f, 0f);

	public void SetupFleets(){
		fleetName = "debugFleet";
	}





	void DrawPath(Vector3 start, Vector3 end, Color color, float duration)
	{
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(color, color);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
		GameObject.Destroy(myLine, duration);
	}



	public  void processMovement(){

		if (targetPosition != null && targetPosition != position && !((Vector3.Distance(targetPosition,position) < 10) && angularThrustEfficiency < .95)&& !((Vector3.Distance(targetPosition,position) < 20) && angularThrustEfficiency < .85)&& !((Vector3.Distance(targetPosition,position) < 30) && angularThrustEfficiency < .75)) {
			if (angularThrustEfficiency >= .92) {
				position = Vector3.MoveTowards (position, targetPosition + fleetGo.transform.up*(1-angularThrustEfficiency), movementSpeed * Time.deltaTime*angularThrustEfficiency);

			}
			if (angularThrustEfficiency >= .84 && angularThrustEfficiency < .92) {
				position = Vector3.MoveTowards (position, targetPosition + fleetGo.transform.up*(3-angularThrustEfficiency), movementSpeed * Time.deltaTime*angularThrustEfficiency);

			}
			if (angularThrustEfficiency >= .78 && angularThrustEfficiency < .84) {
				position = Vector3.MoveTowards (position, targetPosition + fleetGo.transform.up*(5-angularThrustEfficiency), movementSpeed * Time.deltaTime*angularThrustEfficiency);

			}
			if (angularThrustEfficiency >= .72 && angularThrustEfficiency < .78) {
				position = Vector3.MoveTowards (position, targetPosition + fleetGo.transform.up*(8-angularThrustEfficiency), movementSpeed * Time.deltaTime*angularThrustEfficiency);

			}
			if (angularThrustEfficiency >= .64 && angularThrustEfficiency < .72) {
				position = Vector3.MoveTowards (position, targetPosition + fleetGo.transform.up*(13-angularThrustEfficiency), movementSpeed * Time.deltaTime*angularThrustEfficiency);

			}
			if (angularThrustEfficiency >= .58 && angularThrustEfficiency < .64) {
				position = Vector3.MoveTowards (position, targetPosition + fleetGo.transform.up*(16-angularThrustEfficiency), movementSpeed * Time.deltaTime*angularThrustEfficiency);

			}
			if (angularThrustEfficiency >= .5 && angularThrustEfficiency < .6) {
				position = Vector3.MoveTowards (position, targetPosition + fleetGo.transform.up*(18-angularThrustEfficiency), movementSpeed * Time.deltaTime*angularThrustEfficiency);

			}
			if (angularThrustEfficiency >= .4 && angularThrustEfficiency < .5) {
				position = Vector3.MoveTowards (position, targetPosition + fleetGo.transform.up*(28-angularThrustEfficiency), movementSpeed * Time.deltaTime*angularThrustEfficiency);

			}
			}


	}





	//______________________________________________________________________
	GameObject SpritesGo;  //these two lines prepare sprites to be loadedf rom the container object
	Sprites sprites;

	public string fleetName;

	List<Ships>FleetShips = new List<Ships>(); //all ships in this fleet

	public bool removeSlowShips = false; //TODO

	public bool registered = false; //has been added to the list of all fleets

	public GameObject fleetGo;


	public float movementSpeed; //current move speed
	public float mMovementSpeed; //max move speed

	float angularThrustEfficiency = 1;


	Vector3 position; //coordinates on map
		public Vector3 Position {
		get {
			return position;
		}
		set {
			value.z = 0;
			position = value;
			updateMovement (); //whenever fleet psition is changed this function will be called
		}
	}

	Vector3 targetPosition;

	public Vector3 TargetPosition {
		get {
			return targetPosition;
		}
		set {
			targetPosition = value;
			if (targetPosition != null) {
				LookTowards ();
			}

		}
	}

	Quaternion targetRot;

	void LookTowards(){
		Vector3 dir = targetPosition - position;
		var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90; //subtract 90 because our sprites front is up instead of right

		//fleetGo.transform.rotation;
		targetRot = Quaternion.AngleAxis (angle, Vector3.forward);
	}

	void updateMovement(){
		foreach (Ships ship in FleetShips) {
			if (ship.movementSpeed >= movementSpeed) { //check that the ship is actually capable of the movement
				ship.Position = position; //update its position
			} else if(removeSlowShips){
				removeFromFleet (ship); //this should never happen
			}
			fleetGo.transform.position = position;
		}
	}

	void removeFromFleet(Ships ship){
		FleetShips.Remove (ship);
		ship.assignedFleet = null;
	}



	void setMovementSpeed(){
		float max = 10;

		foreach (Ships ship in FleetShips) {
			if (ship.movementSpeed < max) { //lower max movement speed to whatever the slowest ships is
				max = ship.movementSpeed;
			}
		}
		mMovementSpeed = max;
		if (movementSpeed > mMovementSpeed) {
			movementSpeed = mMovementSpeed;
		}
		movementSpeed = mMovementSpeed; //TODO
	}






	// Use this for initialization
	void Start () {
		setupGo ();
		SetupFleets ();
		FleetShips = new List<Ships> ();
		FleetsList = GameObject.FindGameObjectWithTag ("Lists").GetComponent<Lists>().FleetsList;
		AddList ();


		Debug.Log ("Fleet created: " + fleetName);

	}

	void setupGo(){
		fleetGo = this.gameObject;
		if (fleetGo == null) {
			Debug.Log ("Fatal GameObject load error");
		}
		position = new Vector3(fleetGo.transform.position.x, fleetGo.transform.position.y,0);
		targetPosition = position;
		SpritesGo = GameObject.FindGameObjectWithTag ("GameController");
		sprites = SpritesGo.GetComponent<Sprites>();
		fleetSprite = sprites.FleetSprite;
		if (fleetSprite == null) {
			Debug.Log ("Fleet sprite load error");
		}

		fleetGo.transform.position.Set(position.x, position.y, position.z);
		fleetGo.AddComponent<SpriteRenderer>();
		fleetGo.GetComponent<SpriteRenderer> ().sprite = fleetSprite;
		//fleetGo.transform.position = new Vector3 (position.x, position.y, 0);

	}

	void AddList(){
		FleetsList.Add (this);
		registered = true;
		Debug.Log (FleetsList.Count);
	}


	// Update is called once per frame
	void Update() {

		if (!localPlayerAuthority) {
			return;
		}

		setMovementSpeed ();
	

		//Cancer math sorry__________________
		

		fleetGo.transform.rotation = Quaternion.Slerp (fleetGo.transform.rotation, targetRot, .5f*Time.deltaTime);

		angularThrustEfficiency = 1f - (Quaternion.Angle (targetRot, fleetGo.transform.rotation)) / 180f;

		/*
		if (angularThrustEfficiency < .5f)
		{		angularThrustEfficiency = 0f;
	}
		if (angularThrustEfficiency < .75f)
		{		angularThrustEfficiency /= 4f;
		}
		if (angularThrustEfficiency < .90f)
		{		angularThrustEfficiency /= 3f;
		}
		if (angularThrustEfficiency < .96f)
		{		angularThrustEfficiency /= 2f;
		}

*/
		//Debug.Log (angularThrustEfficiency); //this will spam the shit out of the log
		//___________________________________

		processMovement ();
		fleetGo.transform.position = position;
		if (position != targetPosition) {
			DrawPath (position, targetPosition, Color.green, Time.deltaTime);
		}

	}





}
