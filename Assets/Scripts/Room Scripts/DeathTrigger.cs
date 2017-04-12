using UnityEngine;

public class DeathTrigger : MonoBehaviour {
	public GameObject platform, switchh;
	public Transform spawnPoint, platformStart;
	bool reset;
	Character player;

	void Start() {
		player = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
	}

	void LateUpdate() {
		if (!player.getAlive() && !reset) {
			Invoke ("resetRoom", 3.0f);
			reset = true;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "player") {
			player.takeDamage (10);
			resetRoom();
		}
	}

	void resetRoom() {
		switchh.GetComponent<pStartThisRooom>().Reset();
		player.gameObject.transform.position = spawnPoint.position;
		platform.transform.position = platformStart.position;
		reset = false;
	}
}