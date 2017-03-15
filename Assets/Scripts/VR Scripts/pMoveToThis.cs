using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pMoveToThis : MonoBehaviour {
    public Transform target;
    public float speed;
    public bool startButton = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (startButton == true){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}
