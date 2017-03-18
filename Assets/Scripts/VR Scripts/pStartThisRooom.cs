using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pStartThisRooom : MonoBehaviour {
    private pMoveToThis pMove;
	// Use this for initialization
	void Start () {
        pMove = gameObject.GetComponentInParent<pMoveToThis>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other) {
        pMove.startButton = true;
    }
   /* private void OnTriggerStay(Collider other) {
        pMove.startButton = true;
    }
    private void OnTriggerExit(Collider other) {
        pMove.startButton = false;
    }*/
}
