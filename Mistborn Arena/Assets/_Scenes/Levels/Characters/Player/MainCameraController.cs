using UnityEngine;
using System.Collections;

public class MainCameraController : MonoBehaviour {

	float initialZ;
	float initialY;
	public GameObject player;
	private MovementController mControl;
	private bool rotationLock;
	public NetworkView netView;

	void Start ()
	{
		netView = GetComponent<NetworkView>();
		mControl = player.GetComponent<MovementController> ();
		if (netView.isMine) {
			mControl.cam.enabled = true;
		}
		if (!netView.isMine) {
			mControl.cam.enabled = false;
		}
	}

	void Update(){
	}

	void FixedUpdate()
	{
	}

	void LateUpdate ()
	{

	}
}
