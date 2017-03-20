using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public Transform start, end;
	int numActivations = 2, totalActivations;
	float speed;
	bool activated;
    public GameObject vrPlayer;
    private MoveIfDead mid;
    // Use this for initialization
   
    void Awake() {
        mid = vrPlayer.gameObject.GetComponent<MoveIfDead>(); 
        activated = true;
		speed = 5.0f;
	}

	void FixedUpdate() {
		if (activated) {
			transform.position = Vector3.MoveTowards (transform.position, end.position, speed * Time.deltaTime);
            mid.rRoom = true;
        } else {
			transform.position = Vector3.MoveTowards (transform.position, start.position, speed * Time.deltaTime);
		}
	}

	public void setActivation(bool active) {
		if (active) {
			totalActivations++;
			if (totalActivations == numActivations) {
				activated = active;
			}
		} else {
			if (totalActivations != 0) {
				totalActivations--;
			}
			activated = active;
		}
	}
}