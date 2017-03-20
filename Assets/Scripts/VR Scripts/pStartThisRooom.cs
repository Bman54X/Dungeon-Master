using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pStartThisRooom : MonoBehaviour {
    private pMoveToThis pMove;
	MeshRenderer mesh;

	// Use this for initialization
	void Start () {
        pMove = gameObject.GetComponentInParent<pMoveToThis>();
		mesh = GetComponent<MeshRenderer>();
	}

	public void Reset() {
		mesh.material.color = Color.red;
		pMove.startButton = false;
	}

    private void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag ("Arrow")) {
			pMove.startButton = true;
			mesh.material.color = Color.green;
			Destroy (other.gameObject);
		} else if (other.gameObject.CompareTag ("Sword")) {
			Sword s = other.gameObject.GetComponent<Sword> ();
			if (!s.hitOnce && s.swinging) {
				pMove.startButton = true;
				s.hitOnce = true;
				mesh.material.color = Color.green;
			}
		}
    }
}