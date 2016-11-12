using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arm : Bone {



	public bool destroyed = false;
	public int position;  

	public List<Artery> ArmArteries = new List<Artery> ();
//	public List<Artery> HandArteries = new List<Artery> ();

	public List<Bone> ArmBones = new List<Bone>();
	public List<Bone> Hand = new List<Bone>();

	public Bone Scapula = new Bone (.8, 25, 1);
	public Bone Humerus = new Bone (.75, 20, 2);
	public Bone Radius = new Bone (.45, 15, 3);
	public Bone Ulna = new Bone (.45, 15, 3);
	public Bone Carpus = new Bone (.5, 20, 4);
	public Bone Thumb = new Bone (.2, 10, 5);
	public Bone Pointer = new Bone (.2, 10, 5);
	public Bone Middle = new Bone (.2, 10, 5);
	public Bone Ring = new Bone (.2, 10, 5);
	public Bone Pinky = new Bone (.2, 5, 5);

	public Artery Axillary = new Artery (1, 25);
	public Artery Brachial = new Artery (2, 15);
	public Artery Ulnar = new Artery (3, 10);
	public Artery Radial = new Artery (3, 10);


	public Arm(int p, int g){
		if(p > 9 || p < 1)
		{
			throw new System.OverflowException("Position: " + p + " is invalid for arm!");
		}
		position = p;

		switch(g){
		case MALE: //placeholder
			{
				break;
			}
		case FEMALE: //females have weaker bone structure than males
			{
				Bone Scapula = new Bone (.7, 20, 1);
				Bone Humerus = new Bone (.65, 14, 2);
				Bone Radius = new Bone (.25, 10, 3);
				Bone Ulna = new Bone (.25, 10, 3);
				Bone Carpus = new Bone (.35, 12, 4);
				Bone Thumb = new Bone (.12, 8, 5);
				Bone Pointer = new Bone (.12, 8, 5);
				Bone Middle = new Bone (.12, 8, 5);
				Bone Ring = new Bone (.12, 8, 5);
				Bone Pinky = new Bone (.12, 5, 5);
				break;
			}

		}
	

	}
	public Arm(int p, Entity.Gender g){
		if(p > 9 || p < 1)
		{
			throw new System.OverflowException("Position: " + p + " is invalid for arm!");
		}
		position = p;

		switch(g){
		case Entity.Gender.MALE: //placeholder
			{
				break;
			}
		case Entity.Gender.FEMALE: //females have weaker bone structure than males
			{
				Bone Scapula = new Bone (.7, 20, 1);
				Bone Humerus = new Bone (.65, 14, 2);
				Bone Radius = new Bone (.25, 10, 3);
				Bone Ulna = new Bone (.25, 10, 3);
				Bone Carpus = new Bone (.35, 12, 4);
				Bone Thumb = new Bone (.12, 8, 5);
				Bone Pointer = new Bone (.12, 8, 5);
				Bone Middle = new Bone (.12, 8, 5);
				Bone Ring = new Bone (.12, 8, 5);
				Bone Pinky = new Bone (.12, 5, 5);
				break;
			}

		}


	}





	// Use this for initialization
	void Start () {
		ArmBones.Add(Scapula);
		ArmBones.Add(Humerus);
		ArmBones.Add(Radius);
		ArmBones.Add(Ulna);
		ArmBones.Add(Carpus);
		ArmBones.Add(Thumb);
		ArmBones.Add(Pointer);
		ArmBones.Add(Middle);
		ArmBones.Add(Ring);
		ArmBones.Add(Pinky);

		ArmArteries.Add (Axillary);
		ArmArteries.Add (Brachial);
		ArmArteries.Add (Radial);
		ArmArteries.Add (Ulnar);


		Hand.Add(Carpus);
		Hand.Add(Thumb);
		Hand.Add(Pointer);
		Hand.Add(Middle);
		Hand.Add(Ring);
		Hand.Add(Pinky);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
