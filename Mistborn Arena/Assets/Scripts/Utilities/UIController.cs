using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIController : MonoBehaviour {

	public Text arsenalText;
	private string arsenalTextString;

	public GameObject player;
	public Component abS;




	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		arsenalText = Text.FindObjectOfType<Text>();
	//	abS = player.GetComponent (Abilities);
		arsenalTextString = "UI: Initialized";
	}
	
	// Update is called once per frame
	void Update () {

	//	arsenalTextString = "Press e to use vision and enable Iron/Steel" + "\n(V)Iron: " + player.Abilities.Arsenal.GetValue(CharacterProperties.Element.IRON) + "\n(C)Steel: " + Abilities.Arsenal.GetValue(CharacterProperties.Element.STEEL); //+ "\nTin: " + Arsenal.GetValue(TIN)+ "\n(T) Pewter: " + Arsenal.GetValue(PEWTER)+ "\nZinc: " + Arsenal.GetValue(ZINC)+ "\nBrass: " + Arsenal.GetValue(BRASS)+ "\nCopper: " + Arsenal.GetValue(COPPER)+ "\nBronze: " + Arsenal.GetValue(BRONZE)+ "\nChromium: " + Arsenal.GetValue(CHROMIUM)+ "\nDuralumin: " + Arsenal.GetValue(DURALUMIN)+ "\nAtium: " + Arsenal.GetValue(ATIUM) + "\nCoins: " + coins + "\nPewter: " + pewterActive + "\nMspeed: " + mSpeed + "\nBurnRate: " + BurnRate[PEWTER] +"\n(R)Flaring: " + isFlaring + "\nFlare CD: " + uFlareCD + "\n (F)Jump";
		arsenalText.text = arsenalTextString ; //display remaining metals and such
	
	}
}
