using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocksc : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void OnTriggerEnter(Collider c) {
        if (c.transform.name == "Platform") {
            GameObject emptyObject = new GameObject();
            emptyObject.transform.parent = c.transform;
            transform.parent = emptyObject.transform;
        }
    }
}
