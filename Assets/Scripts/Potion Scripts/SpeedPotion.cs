using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : Potion {
	public SpeedPotion() {
		potionTag = "speed";
		inventory = 5;
		character = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
	}

	public override bool potionEffect() {
		character._speedMultiplier = 2;
		character._potionActivated = true;
		return true;
	}

	public override void deactivate() {
		character._speedMultiplier = 1;
	}
}