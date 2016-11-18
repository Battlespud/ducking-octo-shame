using UnityEngine;
using System.Collections;
/*
 * Physical 
 * HP - Hit point capacity
 * STR - Physical Attack Power (Counters RES)
 * RES - Resilience (Counters STR)
 * 
 * Ability
 * AP - Ability Point Capacity (Similar to HP, when the bar reachs a certain threshold, the player can activate their ability. Having their ability active slowly drains their AP pool, and using special movies significantly drains its.)
 * POW - Power of ability moves (Counters DEF)
 * DEF - Defense against ability moves (Counters POW)
 * 
 * Character
 * SPT - Sprint (Run) speed
 * SPG - Spring (Jump) Height
 */
public class PlayerTypes {
	//physical
	int hp;
	int str;
	int res;
	//ability
	int ap;
	int pow;
	int def;
	//character
	int spt;
	int spg;
}
