using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionUI : MonoBehaviour {
	public Character player;
	Character.Potions currentPotion, previousPotion;
	int potionAmount;

	public GameObject[] potions = new GameObject[4];

	Dictionary<Character.Potions, GameObject> potionImages = new Dictionary<Character.Potions, GameObject>();

	// Use this for initialization
	void Start () {
		currentPotion = player.getPotion ();
		potionAmount = player.getPotionAmount ();
		previousPotion = currentPotion;

		potionImages.Add (Character.Potions.HEALTH, potions[0]); potionImages.Add (Character.Potions.DEFENSE, potions[1]); 
		potionImages.Add (Character.Potions.SPEED, potions[2]); potionImages.Add (Character.Potions.GOLD, potions[3]);

		for (int i = 0; i < 4; i++) {
			potions [i].SetActive (false);
		}

		potionImages [currentPotion].SetActive (true);
	}

	void Update() {
		if (player.getChangedPotions()) {
			changedPotions (player.getPotion(), player.getPotionAmount());
		}
	}
	
	void changedPotions(Character.Potions newPotion, int numPotions) {
		if (newPotion != currentPotion) {
			currentPotion = newPotion; potionAmount = numPotions;
			potionImages [currentPotion].SetActive (true);
			potionImages [previousPotion].SetActive (false);
			previousPotion = currentPotion;
		}
	}
}