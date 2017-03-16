using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour {
	public Transform start, end;
	Transform target;
	bool startIsTarget;
	float waitTime, counter, speed;

	// Use this for initialization
	void Start () {
		target = start;
		startIsTarget = true;
		speed = 5.0f;
		waitTime = 3.0f; counter = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
		if (transform.position == target.position) {
			counter += Time.deltaTime;
			if (counter >= waitTime) {
				changeTarget ();
			}
		}
	}

	void changeTarget() {
		counter = 0.0f;
		if (startIsTarget) {
			target = end;
		} else {
			target = start;
		}
		startIsTarget = !startIsTarget;

		transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	}
}