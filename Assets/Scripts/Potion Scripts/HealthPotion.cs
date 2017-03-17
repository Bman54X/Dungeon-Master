using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion {
	public HealthPotion() {
		potionTag = "health";
		character = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
		inventory = 3;
	}

	public override bool potionEffect() {
		int h = character.health;
		int mh = character._maxHealth;

		if (h < mh) {
			h += 50;
			if (h > mh) {
				h = mh;
			}
			character.health = h;
			return true;
		} else {
			return false;
		}
	}
}