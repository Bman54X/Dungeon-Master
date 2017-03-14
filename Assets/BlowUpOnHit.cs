using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUpOnHit : MonoBehaviour {
    public bool objectThrown = false;
    private pObjectManager pobjectM;
    public GameObject pObjectMan;
	// Use this for initialization
	void Start () {

        pobjectM = pObjectMan.GetComponent<pObjectManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void setTrue()
    {
        pobjectM.bomb = true;
        objectThrown = true;
    }

}
