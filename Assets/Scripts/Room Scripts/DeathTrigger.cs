using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {
	public GameObject platform, switchh;
	public Transform spawnPoint, platformStart;
	bool reset;
	Character c;

	void Start() {
		c = GameObject.FindGameObjectWithTag ("player").GetComponent<Character>();
	}

	void LateUpdate() {
		if (!c.getAlive () && !reset) {
			Invoke ("resetRoom", 3.0f);
			reset = true;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "player") {
			c.takeDamage (10);
			resetRoom();
		}
	}

	void resetRoom() {
		switchh.GetComponent<pStartThisRooom>().Reset();
		c.gameObject.transform.position = spawnPoint.position;
		platform.transform.position = platformStart.position;
		reset = false;
	}
}