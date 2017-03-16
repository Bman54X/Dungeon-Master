using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
	MeshRenderer mesh;
	bool activated;

	// Use this for initialization
	void Start () {
		activated = false;
		mesh = GetComponent<MeshRenderer>();
		mesh.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Arrow")) {
			if (!activated) {
				mesh.material.color = Color.green;
			} else {
				mesh.material.color = Color.red;
			}
			activated = !activated;
			Destroy (other.gameObject);
		}
	}

	public bool getActivation() {
		return activated;
	}
}