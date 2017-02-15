using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : Potion {
	void Start() {
		potionTag = "speed";
		inventory = 5;
		character = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
	}

	public override void potionEffect() {
		character._speedMultiplier = 2;
		character._potionActivated = true;
	}
}