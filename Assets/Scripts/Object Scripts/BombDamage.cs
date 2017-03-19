using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
       // Debug.Log("stay trigger");
    }
    private void OnTriggerEnter(Collider other)
    {
     //   Debug.Log("trigger enter");
    }
}
