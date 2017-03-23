using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript1 : MonoBehaviour {
  //  public GameObject explosionPrefab;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<BoxCollider>().center = new Vector3(0, -4.5f, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
