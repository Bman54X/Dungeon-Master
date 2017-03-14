using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIfDead : MonoBehaviour {
    public bool dead = false;
    public Transform deadP;
    public Transform roomP;
    public Transform VRPLAT;
	// Use this for initialization
	void Start () {
        transform.position = VRPLAT.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (dead == true)
        {
          //  transform.position = deadP.position;
        }
        if(dead == false)
        {
          // transform.position = roomP.position;
         
        }
	}
}
