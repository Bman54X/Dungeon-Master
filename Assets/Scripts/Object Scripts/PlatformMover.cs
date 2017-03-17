using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour {
	public Transform start, end;
	Transform target;
	bool startIsTarget;
	float waitTime, counter, speed;
	bool activated;

	// Use this for initialization
	void Awake() {
		activated = true;
		target = start;
		startIsTarget = true;
		speed = 5.0f;
		waitTime = 3.0f; counter = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if (activated) {
			transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
			if (transform.position == target.position) {
				counter += Time.deltaTime;
				if (counter >= waitTime) {
					changeTarget ();
				}
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

	public void setActivation(bool active) {
		activated = active;
	}
}