using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePotion : Potion {
	// Use this for initialization
	void Start () {
		potionTag = "defense";
		inventory = 8;
		character = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
	}

	public override void potionEffect() {
		character._potionActivated = true;
	}
}