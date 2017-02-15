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
	void Start () {
		inventory = 0;
		character = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
		potionTag = "";
	}

	public void changeInventory(int num) {
		inventory += num;
	}

	public int getInventory() {
		return inventory;
	}

	public virtual void potionEffect() {
	}
}