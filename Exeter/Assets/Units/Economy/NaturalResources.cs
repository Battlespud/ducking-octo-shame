using UnityEngine;
using System.Collections;

public class NaturalResources : NonShipEntity {


	//TODO
	//set mass and volume per unit and rebalance once cargo components are designed

	public enum NaturalResourcesType{ STEEL, SILICONE, SILVER}; //TODO add more later
	public NaturalResourcesType NaturalResourceType;

	//how effective mining this resource will be, between .01 - 1.0 
	//If a mine is rated for 1000 units per turn, and accessibility is .5, it will only mine 500 units of this resource.
	float accessibility; 

	public float amountUnavailable; //how much of this resource is in the ground
	public float amountAvailable; //how much has been mined and is ready for immediate use

	public NaturalResources( NaturalResourcesType t, float amountUn, float amountA = 0,  float access = 1){
		NaturalResourceType = t;
		type = NonShipEntityType.RESOURCE;

		accessibility = access;
		amountAvailable = amountA;
		amountUn = amountUnavailable;

		volumePerUnit = 1;
		massPerUnit = 1;
	}




}
