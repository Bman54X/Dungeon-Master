using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
	MeshRenderer mesh;
	bool activated;
	public PlatformMover platform;

	// Use this for initialization
	void Start () {
		activated = false;
		mesh = GetComponent<MeshRenderer>();
		mesh.material.color = Color.red;
		platform.setActivation (false);
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
		platform.setActivation (activated);
	}
}