using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Leg : Bone {



	public int position;  

	public List<Bone> LegBones = new List<Bone>();

	public Bone Femur = new Bone (.8, 35, 1);
	public Bone Patella = new Bone (.75, 20, 2, false);
	public Bone Tibia = new Bone (.45, 15, 3);
	public Bone Fibula = new Bone (.25, 10, 3);



	public Leg(int p, int g){
		if(p > 9 || p < 1)
		{
			throw new System.OverflowException("Position: " + p + " is invalid for Leg!");
		}
		position = p;

		switch(g){
		case MALE: //placeholder
			{
				break;
			}
		case FEMALE: //females have weaker bone structure than males
			{
				 Bone Femur = new Bone (.6, 25, 1);
				 Bone Patella = new Bone (.55, 10, 2, false);
				 Bone Tibia = new Bone (.35, 11, 3);
				 Bone Fibula = new Bone (.2, 8, 3);
				break;
			}

		}

	}
	public Leg(int p, Entity.Gender g){
		if(p > 9 || p < 1)
		{
			throw new System.OverflowException("Position: " + p + " is invalid for Leg!");
		}
		position = p;

		switch(g){
		case Entity.Gender.MALE: //placeholder
			{
				break;
			}
		case Entity.Gender.FEMALE: //females have weaker bone structure than males
			{
				Bone Femur = new Bone (.6, 25, 1);
				Bone Patella = new Bone (.55, 10, 2, false);
				Bone Tibia = new Bone (.35, 11, 3);
				Bone Fibula = new Bone (.2, 8, 3);
				break;
			}

		}


	}






	// Use this for initialization
	void Start () {
		LegBones.Add(Femur);
		LegBones.Add(Patella);
		LegBones.Add(Tibia);
		LegBones.Add(Fibula);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
