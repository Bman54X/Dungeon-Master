using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion {
	void Start() {
		potionTag = "health";
		character = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
		inventory = 3;
	}

	public override void potionEffect() {
		int h = character.health;
		h += 50;
		if (h > character._maxHealth) {
			h = character._maxHealth;
		}
		character.health = h;
	}
}
