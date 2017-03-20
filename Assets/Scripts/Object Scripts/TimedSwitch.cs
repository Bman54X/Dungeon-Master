using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSwitch : MonoBehaviour {
	MeshRenderer mesh;
	bool activated;
	public Door door;
	float resetTime = 5.0f, counter = 0.0f;
	public TimedSwitch otherSwitch;
 

    // Use this for initialization
    void Start () {
        
        activated = false;
		mesh = GetComponent<MeshRenderer>();
		mesh.material.color = Color.red;
		door.setActivation (false);
	}

	void Update() {
		if (activated && !otherSwitch.getActivated()) {
			counter += Time.deltaTime;
			if (counter >= resetTime) {
				counter = 0.0f;
				activated = false;
				mesh.material.color = Color.red;
				door.setActivation(activated);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Arrow")) {
			switchActivate();
			Destroy (other.gameObject);
		}
	}

	public void switchActivate() {
		if (!activated) {
			mesh.material.color = Color.green;
		} else {
			mesh.material.color = Color.red;
		}
		activated = !activated;
		door.setActivation (activated);
	}

	public bool getActivated() {
		return activated;
	}
}