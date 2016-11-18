using UnityEngine;
using System.Collections;

public class KawaiiCharController: MonoBehaviour {

	public enum ControlScheme
	{
		STANDARD,
		MMO,
		RESIDENT

	};

	private ControlScheme controlScheme;

	int width;
	int height;

	public bool debug;
	public Transform linecastOrigin, jumpCheck; //transform variable for the end points of the linecasts

	public GameObject camera;
	public GameObject rotProxy;
	public MainCameraController cameraScript;
	
	private float speed;   // rb.velocity.magnitude
	private const float omSpeed = 21.5f; //original max speed
	private float mSpeed;  //current max speed
	
	public float jumpForce;
	
	public PhysicMaterial playerFriction; //TODO drag over on inspector
	
	private float jumpTime, jumpDelay = .3f;
	private float moveHorizontal, moveVertical;
	private bool grounded;
	
	private Rigidbody rb;  //player's rigidbody.
	private Animator anime; //nis mobacayonanimator.
	private AnimatorStateInfo animeInfo; //For getting info about which state the animator is in

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;;
		//Application.targetFrameRate = 600;
		height = Screen.currentResolution.height;
		width = Screen.currentResolution.width;
		Debug.Log ("W: " + width + " H: " + height);
		rb = gameObject.GetComponent<Rigidbody>(); //k then
		anime = gameObject.GetComponent<Animator> ();
		rotProxy = GameObject.FindGameObjectWithTag ("rotProxy");
		 camera = GameObject.FindGameObjectWithTag ("MainCamera");
		cameraScript = camera.GetComponent<MainCameraController> ();
		mSpeed = omSpeed;
		controlScheme = ControlScheme.MMO;
	}
	
	// FixedUpdate is called once per frame except for PHYSX!
	void FixedUpdate(){
		Rotation ();
		Movement();
	}
	//END FIXED UPDATE
	
	// Update is called once per frame
	void Update () {
		RaycastStuff();
		JumpChecking();
		HandleMovementAnimations();
	}
	//END UPDATE

	void Rotation(){
//		Debug.Log ("Mouse: " + Input.mousePosition);
		if (Input.GetAxis("MouseX")  < 0) {
				rotateLeft();
			//Debug.Log ("Left");
		}
		if (Input.GetAxis ("MouseX") > 0) {
				rotateRight();
		//	Debug.Log ("Right");
			
		}

	}

	void rotateLeft(){
		rb.transform.Rotate(new Vector3(0, -3.5f, 0));

	}


	void rotateRight(){
		rb.transform.Rotate(new Vector3(0, 3.5f, 0));
	}
	
	
	void Movement(){
		//DIRECTIONAL MOVEMENT
		switch (controlScheme) {

		case ControlScheme.STANDARD:
			StandardControls();
			break;
		case ControlScheme.RESIDENT:
			ResidentControls();
			break;
		case ControlScheme.MMO:
			MMOControls();
			break;

		}

	}
	
	
	void  HandleMovementAnimations(){
		//Debug.Log (rb.velocity.magnitude);
		anime.SetBool ("Running", (rb.velocity.magnitude > 5.0f));
		anime.SetFloat ("Speed", rb.velocity.magnitude);
		anime.SetFloat ("HSpeed", moveHorizontal);
		anime.SetFloat ("VSpeed", moveVertical);
	}
	
	void JumpChecking(){
		//JUMPING
		if(Input.GetKeyDown (KeyCode.Space) && grounded) // If the jump button is pressed and the player is grounded then the player jumps 
		{
			rb.AddForce(transform.up * jumpForce);
			jumpTime = jumpDelay;
			anime.SetTrigger("Jump");
		}
		
		//LANDING
		jumpTime -= Time.deltaTime;
		if(jumpTime <= 0 && grounded)
		{
			//any landing logic goes here
			anime.SetTrigger("Land");
		}
	}

	public void MMOControls(){

		moveHorizontal = Input.GetAxis ("Horizontal"); //get A D input
		moveVertical = Input.GetAxis ("Vertical"); //get W S input
		
		if (Input.GetKey(KeyCode.D)) {
			moveHorizontal = 1;
		}
		if (Input.GetKey(KeyCode.A)) {
			moveHorizontal = -1;
		}
		
		if (Input.GetKey(KeyCode.W)) {
			moveVertical = 1;
		} else if (Input.GetKey(KeyCode.S)) {
			moveVertical = -1;
		}
		
		//modify speed based on direction
		if (moveVertical < 0) {  //if moving backwards, slow slightly
			mSpeed *= .80f;
			//Debug.Log("Moving Back and slowing");
		}
		if (moveVertical == 0) { //if strafing, slow slightly
			mSpeed *= .82f;
			//Debug.Log("Strafing and slowing");
		} 
		if (moveHorizontal == 0 && moveVertical == 0) {
			playerFriction.dynamicFriction = 10.5f;
		} else {
			playerFriction.dynamicFriction = 1.8f;
		}
		if (moveVertical < 0) {
			//playerFriction.dynamicFriction *= 2.85f; //reduce movement speed backwards
		}
		if (grounded) {
			rb.drag = .1f;
		} else {
			rb.drag = .5f; //TODO tune values
		}
		
		Vector3 direction = new Vector3 (moveHorizontal, 0f, moveVertical);
		
		if ( rb.velocity.magnitude + ((float)mSpeed*direction.magnitude/(float)rb.mass) > 18.5f && direction.magnitude != 0){  //limits speed
			mSpeed = ((float)rb.mass*(mSpeed-(float)rb.velocity.magnitude))/(float)direction.magnitude;
		}
		//Vector3 lookTo = new Vector3 ((float)((double)rb.transform.position.x + (double)rb.velocity.x / 20000), rb.position.y, (float)((double)rb.transform.position.z + (double)rb.velocity.z / 20000));
		//cameraScript.collectRotation (lookTo); //pass off to camera, TODO
		Vector3 dir = camera.transform.TransformDirection (direction);
		dir.y = 0f;
	//	transform.rotation = camera.transform.rotation;
		rb.AddForce(dir.normalized*direction.magnitude*mSpeed);
	//	Debug.Log (direction * mSpeed);
		mSpeed=omSpeed;

	}

	public void StandardControls(){
		moveHorizontal = Input.GetAxis ("Horizontal"); //get A D input
		moveVertical = Input.GetAxis ("Vertical"); //get W S input
		
		if (Input.GetKey(KeyCode.D)) {
			moveHorizontal = 1;
		}
		if (Input.GetKey(KeyCode.A)) {
			moveHorizontal = -1;
		}
		
		if (Input.GetKey(KeyCode.W)) {
			moveVertical = 1;
		} else if (Input.GetKey(KeyCode.S)) {
			moveVertical = -1;
		}
		
		//modify speed based on direction
		if (moveVertical < 0) {  //if moving backwards, slow slightly
			mSpeed *= .80f;
			//Debug.Log("Moving Back and slowing");
		}
		if (moveVertical == 0) { //if strafing, slow slightly
			mSpeed *= .85f;
			//Debug.Log("Strafing and slowing");
		} 
		if (moveHorizontal == 0 && moveVertical == 0) {
			playerFriction.dynamicFriction = 8.5f;
		} else {
			playerFriction.dynamicFriction = 1.8f;
		}
		if (moveVertical < 0) {
			//playerFriction.dynamicFriction *= 2.85f; //reduce movement speed backwards
		}
		if (grounded) {
			rb.drag = .1f;
		} else {
			rb.drag = .1f; //TODO tune values
		}
		
		Vector3 direction = new Vector3 (moveHorizontal, 0f, moveVertical);
		
		if ( rb.velocity.magnitude + ((float)mSpeed*direction.magnitude/(float)rb.mass) > 18.5f && direction.magnitude != 0){  //limits speed
			mSpeed = ((float)rb.mass*(22.5f-(float)rb.velocity.magnitude))/(float)direction.magnitude;
		}
		Vector3 lookTo = new Vector3 ((float)((double)rb.transform.position.x + (double)rb.velocity.x / 20000), rb.position.y, (float)((double)rb.transform.position.z + (double)rb.velocity.z / 20000));
	//	transform.LookAt (lookTo); //controls rotation
		//cameraScript.collectRotation (lookTo); //pass off to camera, TODO
		
		//		Debug.Log (mSpeed);
		rb.AddForce(direction*mSpeed);
		//Debug.Log (direction * mSpeed);
		mSpeed=omSpeed;

	}

	public void ResidentControls(){
		moveHorizontal = Input.GetAxis ("Horizontal"); //get A D input
		moveVertical = Input.GetAxis ("Vertical"); //get W S input
		
		if (Input.GetKey(KeyCode.D)) {
			moveHorizontal = 1;
		}
		if (Input.GetKey(KeyCode.A)) {
			moveHorizontal = -1;
		}
		
		if (Input.GetKey(KeyCode.W)) {
			moveVertical = 1;
		} else if (Input.GetKey(KeyCode.S)) {
			moveVertical = -1;
		}
		
		//modify speed based on direction
		if (moveVertical < 0) {  //if moving backwards, slow slightly
			mSpeed *= .80f;
			//Debug.Log("Moving Back and slowing");
		}
		if (moveVertical == 0) { //if strafing, slow slightly
			mSpeed *= .85f;
			//Debug.Log("Strafing and slowing");
		} 
		if (moveHorizontal == 0 && moveVertical == 0) {
			playerFriction.dynamicFriction = 8.5f;
		} else {
			playerFriction.dynamicFriction = 1.8f;
		}
		if (grounded) {
			rb.drag = .1f;
		} else {
			rb.drag = .8f; //TODO tune values
		}
		
		Vector3 direction = new Vector3 (moveHorizontal, 0f, moveVertical);
		
		if ( rb.velocity.magnitude + ((float)mSpeed*direction.magnitude/(float)rb.mass) > 18.5f && direction.magnitude != 0){  //limits speed
			mSpeed = ((float)rb.mass*(22.5f-(float)rb.velocity.magnitude))/(float)direction.magnitude;
		}
		Vector3 lookTo = new Vector3 ((float)((double)rb.transform.position.x + (double)rb.velocity.x / 20000), rb.position.y, (float)((double)rb.transform.position.z + (double)rb.velocity.z / 20000));
		transform.LookAt (lookTo); //controls rotation
		//cameraScript.collectRotation (lookTo); //pass off to camera, TODO
		
		//		Debug.Log (mSpeed);
		rb.AddRelativeForce(direction*mSpeed);
		mSpeed=omSpeed;
	}

	
	void RaycastStuff(){
		//Draws debug shit in the scene
		if(debug){
			Debug.DrawLine(transform.position, jumpCheck.position, Color.magenta);
		}
		
		//we assign the bool 'ground' with a linecast, that returns true or false when the end of line 'jumpCheck' touches any object
		grounded = Physics.Linecast (linecastOrigin.position, jumpCheck.position);
	}
}