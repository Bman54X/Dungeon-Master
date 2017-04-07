using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinArcher : MonoBehaviour {
	int health;
	Animator anim;
	float shootingRange = 50.0f;
	Transform player;
	bool playerClose;

	// Use this for initialization
	void Start () {
		health = 30;
		playerClose = false;
		anim = gameObject.GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Mathf.Abs(Vector3.Distance(player.position, transform.position));

		if (dist <= shootingRange) {
			playerClose = true;
		} else {
			playerClose = false;
		}
	}

	void FixedUpdate() {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Arrow")) {
			if (other.GetComponent<ArrowProjectile>().shooter == "player" && health > 0) {
				anim.SetTrigger ("Hit1");
				takeDamage (10);
			}
			Destroy (other.gameObject);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.CompareTag ("Sword")) {
			Sword sword = other.GetComponent<Sword> ();
			if (sword.swinging && !sword.hitOnce) {
				sword.hitOnce = true;
				takeDamage (30);
			}
		}
	}

	void takeDamage(int damage) {
		health -= damage;
		if (health <= 0) {
			anim.SetTrigger ("Dead");
			Invoke ("DeadGoblin", 5.0f);
		}
	}

	void DeadGoblin() {
		Destroy (gameObject);
	}
}