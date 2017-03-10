using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionUI : MonoBehaviour {
	public Character player;
	int potionAmount, currentPotion, previousPotion;

	public GameObject[] potions = new GameObject[4];
	Text[] potionsTexts = new Text[4];
	public GameObject maxHealth;

	// Use this for initialization
	void Start () {
		currentPotion = player.getPotion();
		previousPotion = currentPotion;

		for (int i = 0; i < 4; i++) {
			potions [i].SetActive (false);
			potionsTexts [i] = potions[i].GetComponentInChildren<Text>();
		}

		potions[currentPotion].SetActive (true);
		maxHealth.SetActive (false);
		potionsTexts[currentPotion].text = player.getPotionAmount().ToString();
	}

	void Update() {
		if (player.getChangedPotions()) {
			changedPotions(player.getPotion(), player.getPotionAmount());
		}

		if (player.getPotionUsed()) {
			potionsTexts[currentPotion].text = player.getPotionAmount().ToString();
		}

		if (player.getMaxHealthAlready ()) {
			maxHealth.SetActive (true);
			Invoke("resetMaxHealth", 1.5f);
		}
	}

	void changedPotions(int newPotion, int numPotions) {
		if (newPotion != currentPotion) {
			currentPotion = newPotion;
			potions [currentPotion].SetActive (true);
			potions [previousPotion].SetActive (false);
			potionsTexts [currentPotion].text = player.getPotionAmount().ToString();
			previousPotion = currentPotion;
		}
	}

	void resetMaxHealth() {
		maxHealth.SetActive (false);
	}
}