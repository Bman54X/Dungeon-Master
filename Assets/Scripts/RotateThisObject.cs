using UnityEngine;
using System.Collections;

public class RotateThisObject : MonoBehaviour {
    public Vector3 RotateThis;
	// Use this for initialization
	void Start () {
        RotateThis = new Vector3(45, 0, 0);

        transform.Rotate(RotateThis.x,RotateThis.y,RotateThis.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
