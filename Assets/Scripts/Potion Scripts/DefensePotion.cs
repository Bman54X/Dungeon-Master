using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePotion : Potion {
	// Use this for initialization
	public DefensePotion() {
		potionTag = "defense";
		inventory = 8;
		character = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
	}

	public override bool potionEffect() {
		character._defense = 2;
		character._potionActivated = true;
		return true;
	}

	public override void deactivate() {
		character._defense = 1;
	}
}