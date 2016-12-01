using UnityEngine;
using System.Collections;

public class Mines : Facilities {


	public NaturalResources.NaturalResourcesType targetResource;



	public Mines(NaturalResources.NaturalResourcesType targResource){
		type = NonShipEntityType.FACILITY;
		facilityType = FacilityType.MINE;
		targetResource = targResource;
		massPerUnit = 1000;
		volumePerUnit = 1000;
	}




}
