using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {


		private float speed = 4f;
		private float oSpeed;
		public CharacterController controller;
		private Vector3 movementVector;
		public NetworkView netView;
		private bool rotationLock, doDash;
		public Camera cam;
		private float initialY, initialZ;
	//dashes
		public float dashes;
		private float  dashTimer, dashRechargeTimer;
		KeyCode dashKey = KeyCode.LeftShift;

		void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		setVariables ();
	}

		void Update()
		{
		if (netView.isMine) {
			DashHandling();
			RotationLocker ();
			InputMovement ();
			Rotation ();
			Zoom ();

			}
		}

		void FixedUpdate(){
		if (netView.isMine) {
			ProcessMovement ();
			Gravity ();
		}
	}

		void DashHandling(){
		dashTimer++;
		if(dashTimer == dashRechargeTimer)
		{
			if(dashes < 3)
			{
				dashes += 1f*Time.deltaTime;
				if (dashes > 3)
				    {
					dashes = 3;
				}
			}
			dashTimer = 0;
		}
		
	}
	void InputMovement()
		{
			if (Input.GetKey(KeyCode.W))
			movementVector = new Vector3(movementVector.x, movementVector.y, movementVector.z+1f);
			if (Input.GetKey (KeyCode.S))
			movementVector = new Vector3(movementVector.x, movementVector.y, movementVector.z-1f);
			if (Input.GetKey (KeyCode.D))
			movementVector = new Vector3(movementVector.x + 1f, movementVector.y, movementVector.z);
			if (Input.GetKey (KeyCode.A))
			movementVector = new Vector3(movementVector.x-1f, movementVector.y, movementVector.z);;
			if (movementVector != new Vector3 (0, 0, 0) && dashes >= 1 && Input.GetKeyDown(dashKey)) { 
			doDash = true;
			}
		}
	void ProcessMovement(){
		if (doDash) {
			speed *= 10;
			dashes--;
			doDash = false;
			Debug.Log ("Dashing");
		}
		controller.Move (transform.TransformDirection(movementVector)*speed*Time.deltaTime);
		movementVector = new Vector3 (0, 0, 0);
		speed = oSpeed;
	}
	void RotationLocker(){
		if (Input.GetKeyDown (KeyCode.CapsLock)) {
			switch (rotationLock)
			{
			case  true:
				rotationLock = false;
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
				break;
			case false:
				rotationLock = true;
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.Confined;
				break;
			}
		}
	}
	void Rotation(){
		if (!rotationLock) {
			if (Input.GetAxis ("Mouse X") < 0) {
				rotateLeft ();
			}
			if (Input.GetAxis ("Mouse X") > 0) {
				rotateRight ();
			}
		}
	}
	void rotateLeft(){
		transform.Rotate(new Vector3(0, -220f, 0)*Time.deltaTime);
		
	}
	void rotateRight(){
		transform.Rotate(new Vector3(0, 220f, 0)*Time.deltaTime);
	}


	void Zoom(){
		if (Input.GetAxis("Mouse ScrollWheel")  > 0) {
			zoomIn();
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			zoomOut();
		}
	}
	
	void zoomIn(){
		if (cam.transform.localPosition.z < .25f*initialZ) {
			cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y - .05f*initialY, cam.transform.localPosition.z + .05f*Mathf.Abs(initialZ));
		}
		
	}
	
	void zoomOut(){
		if ( cam.transform.localPosition.z > initialZ*2f) {
			cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y + .05f*initialY, cam.transform.localPosition.z + .05f*initialZ);		
		}
	}


	void Gravity(){
		controller.Move (Vector3.down * 9.81f*Time.deltaTime);
		}


	void setVariables(){
		oSpeed = speed;
		netView = GetComponent<NetworkView> ();
		controller = GetComponent<CharacterController>();
		movementVector = new Vector3 (0, 0, 0);
		initialY = cam.transform.localPosition.y;
		initialZ = cam.transform.localPosition.z;
		doDash = false;
		dashes = 2;
		dashRechargeTimer = 6;
	}
}

