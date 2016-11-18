using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {




/*			NOTES
 * 		block timing should be adjusted based on weapon chosen and skill
 * 
 * 
 */
	public Collider targetObj;
	//public GameObject hitboxObj;
	//	private Collider hitbox;
		private CharacterClass charClass;
	private CharacterClass otherCharClass;
	private ActionController otherActionController;
	private MovementController mControl;
	private InterfaceController iControl;

	//keys
	private KeyCode blockKey;
	private KeyCode attackKey;
	private KeyCode pushKey;
	private KeyCode pullKey;
	private KeyCode bronzeKey;
	private KeyCode coinShotKey;

	//timers
		//attack
	float attackCD;
	float attackTimer;
	bool  canAttack;
	bool attackWillLand = false;
	float baseDamage;
	float critChance;
	float bleedChance;
		//guard
	bool justGuard;

	NetworkView netview;
	bool initialized = false;


	//



	// Use this for initialization
	void Start () {
		charClass = GetComponent<CharacterClass>();
		mControl = GetComponent<MovementController> ();
		iControl = GetComponent<InterfaceController> ();
		netview = mControl.netView;
	//	hitbox = hitboxObj.GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
	//take inputs
		if (initialized == false && charClass.isInitialized()) {  //makes sure other class is setup before firing, fixes infinity error.
			initialize();
		}
		if (mControl.netView.isMine) {
			if (Input.GetKeyDown (attackKey) && charClass.stamina > 0 && attackTimer == 0) { //TODO add the timer back later
				attack ();
			}
			timers();
		}









	}

	void FixedUpdate(){

	}

	void timers(){
		if (attackTimer > 0) {
			attackTimer = attackTimer - 1*Time.deltaTime;
		}
		if (attackTimer < 0) {
			attackTimer = 0;
		}


	}

	void onTriggerEnter(Collider col){
		Debug.Log ("Bauss!");
		if (col.CompareTag("Player")){
			attackWillLand = true;
			targetObj = col;
			otherCharClass = col.GetComponentInParent<CharacterClass> ();
			otherActionController = col.GetComponentInParent<ActionController>();
		}

	}

	void onTriggerExit(Collider col){
		attackWillLand = false;
		targetObj = null;
		otherCharClass = null;
		otherActionController = null;
	}

	void onTriggerStay(Collider col){
			Debug.Log ("Target in sights!");
	}

	//actions

	void attack(){
		charClass.stamina -= 5;
		canAttack = false;
		attackTimer = attackCD;
		float i = 0;
	//	while (i < .15) {  //force delay before attack completes
	//		i = i + 1*Time.deltaTime;
	//	}
		checkAttack ();

	}

	[RPC]
	void takeDamage(float dam, NetworkMessageInfo mes){
		charClass.health -= dam;
		iControl.Update ();
		Debug.Log (mes.sender + " hit you! Your health is " + charClass.health);
		charClass.name = "ded";
	}

	void checkAttack(){
		RaycastHit hit;
		Debug.Log ("Raycasting");
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*charClass.range, Color.yellow, 1f);
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, charClass.range)) {
			if (hit.collider.CompareTag ("Player")) {
				otherCharClass = hit.collider.GetComponentInParent<CharacterClass> ();
				otherActionController = hit.collider.GetComponentInParent<ActionController>();
				netview.RPC("takeDamage", RPCMode.OthersBuffered, baseDamage); //TODO target
				Debug.Log ("Attack successful" + baseDamage + " " + otherCharClass.health +"/" + otherCharClass.mHealth);
			}
			else{
				Debug.Log ("Thats not a player..");
			}
		} else {
			Debug.Log("Nothing hit");
		}
	}


	void initialize(){ //must fire AFTER character class is initialized
		setControls ();
		setAttackStats ();
	//	setHitbox ();
		initialized = true;
	}


	//
	void setControls(){ //controls, duh
		 blockKey = KeyCode.Mouse1;
		 attackKey = KeyCode.Z;
		 pushKey = KeyCode.C;
		 pullKey = KeyCode.V;
		 bronzeKey = KeyCode.Tab;
		 coinShotKey = KeyCode.F;
	}

	void setHitbox(){ //sets up collider hitbox for calculating weapon hits
	//	hitbox.transform.localScale = new Vector3(hitbox.transform.localScale.x, hitbox.transform.localScale.y, charClass.range);
	//	hitbox.transform.localPosition = new Vector3(hitbox.transform.localPosition.x, hitbox.transform.localPosition.y, charClass.range*.5f);
	//	Debug.Log ("Hitbox set: " + new Vector3(hitbox.transform.localScale.x, hitbox.transform.localScale.y, charClass.range));
	}
	void setAttackStats(){ //imports weapon stats from char class for ease of use.
		 attackCD = 1 / charClass.attackSpeed;
		 attackTimer = 0;
		 canAttack = true;
		 baseDamage = charClass.baseDamage;
		 critChance = charClass.critChance;
		 bleedChance = charClass.bleedChance;
		Debug.Log ("Imported weapon stats: " + attackCD + " " + charClass.range);
	}


}
