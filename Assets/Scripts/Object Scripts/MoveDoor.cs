using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : MonoBehaviour {
	public Transform door;
	public Transform doorStart, doorEnd;
	Transform target;
	float speed = 8.0f;

	// Use this for initialization
	void Start () {
		target = doorStart;
	}
	
	// Update is called once per frame
	void Update () {
		door.position = Vector3.MoveTowards (door.position, target.position, speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "player") {
			changeTarget ();
		}
	}

	public void changeTarget() {
		if (target == doorStart) {
			target = doorEnd;
		} else {
			target = doorStart;
		}
	}
}