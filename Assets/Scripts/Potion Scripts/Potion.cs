using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour {
	[HideInInspector]
	public int inventory;
	[HideInInspector]
	public Character character;
	[HideInInspector]
	public string potionTag;

	// Use this for initialization
	void Awake() {
	}

	public void changeInventory(int num) {
		inventory += num;
	}

	public int getInventory() {
		return inventory;
	}

	public virtual bool potionEffect() {
		return true;
	}

	public virtual void deactivate() {
	}
}