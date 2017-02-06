using UnityEngine;
using System.Collections;

public class RotateThisObject : MonoBehaviour {
    public Vector3 RotateThis;
    public Vector3 MoveThis;
	// Use this for initialization
	void Start () {
        // RotateThis = new Vector3(45, 0, 0);
        transform.localPosition = MoveThis;
       // transform.position.x = MoveThis.x;
        transform.Rotate(RotateThis.x,RotateThis.y,RotateThis.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
