using UnityEngine;

public class SetSpawnPoint : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "player") {
			other.gameObject.GetComponent<Character>().setSpawnPoint(gameObject.transform);
		}
	}
}