using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion {
	[HideInInspector]
	public int inventory;
	[HideInInspector]
	public Character character;
	[HideInInspector]
	public string potionTag;

	public void changeInventory(int num) {
		inventory += num;
	}

	public int getInventory() {
		return inventory;
	}

	public string getTag() {
		return potionTag;
	}

	public virtual bool potionEffect() {
		return true;
	}

	public virtual void deactivate() {
	}
}