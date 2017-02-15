using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPotion : Potion {
	void Start() {
		potionTag = "gold";
		inventory = 4;
		character = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
	}

	public override void potionEffect() {
		character._goldMultiplier = 2;
		character._potionActivated = true;
	}
}