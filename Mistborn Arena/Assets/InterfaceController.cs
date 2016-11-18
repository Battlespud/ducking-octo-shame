using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour {


	public Canvas ui;
		public Text text;
		private MovementController mControl;
		private CharacterClass charClass;
	private NetworkView netview;
		// Use this for initialization
	void Start () {
		text = ui.GetComponent<Text> ();
		mControl = GetComponent<MovementController> ();
		charClass = GetComponent<CharacterClass> ();
		netview = mControl.netView;
	}
	
	// Update is called once per frame
	public void Update () {
		if (netview.isMine) {
			text.text = charClass.name + ": " + charClass.className + "\nHP: " + Mathf.Floor (charClass.health) + "/" + charClass.mHealth + "\nST: " + Mathf.Floor (charClass.stamina) + "/" + charClass.mStamina + "\nDashes: " + Mathf.Floor (mControl.dashes);
		}
	}
}
