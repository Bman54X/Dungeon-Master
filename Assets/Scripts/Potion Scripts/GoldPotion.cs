using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPotion : Potion {
	void Awake() {
		potionTag = "gold";
		inventory = 4;
		character = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
	}

	public override bool potionEffect() {
		character._goldMultiplier = 2;
		character._potionActivated = true;
		return true;
	}

	public override void deactivate() {
		character._goldMultiplier = 1;
	}
}