using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUpOnHit : MonoBehaviour {
    public bool objectThrown = false;
    private pObjectManager pobjectM;
    public GameObject pObjectMan;
	// Use this for initialization
	void Start () {
        pObjectMan = GameObject.FindGameObjectWithTag("pObjectM");
        pobjectM = pObjectMan.GetComponent<pObjectManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(pobjectM.bomb);
	}
    public void setTrue()
    {
        pobjectM.bomb = true;
        objectThrown = true;
    }
    public void setTrueRock() {
        pobjectM.rock = true;
        objectThrown = true;
    }

}
