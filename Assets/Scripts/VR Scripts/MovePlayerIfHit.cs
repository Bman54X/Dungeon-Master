using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerIfHit : MonoBehaviour {
    public bool moveP1 = false;
    public bool moveP2 = false;
    public bool moveP3 = false;
    public bool moveP4 = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Arrow"){
            if (gameObject.name == "MoveToSpot1") {
                moveP1 = true;
            }else
            if (gameObject.name == "MoveToSpot2") {
                moveP2 = true;
            }else
            if (gameObject.name == "MoveToSpot3") {
                moveP3 = true;
            }else
            if (gameObject.name == "MoveToSpot4") {
                moveP4 = true;
            }
        }

    
    }
}
