using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pObjectManager : MonoBehaviour {
    public bool bomb;
    public bool arrow;
    public bool rock;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(bomb == true){
            Debug.Log("bomb resapwn");
        }
	}
}
