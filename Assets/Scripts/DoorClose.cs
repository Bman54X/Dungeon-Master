using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClose : MonoBehaviour {
    public bool doorOpen;
    bool doorMoving;
    float openHeight = 7.5f, closedHeight = 2.5f;
    float moveSpeed = 0.1f;
    Vector3 doorPos;

	// Use this for initialization
	void Start () {
        doorPos = transform.position;
        doorMoving = false;
        if (doorOpen) {
            transform.position = new Vector3(transform.position.x, openHeight, transform.position.z);
        } else {
            transform.position = new Vector3(transform.position.x, closedHeight, transform.position.z);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (doorMoving) {
            if (doorOpen) {
                transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed, transform.position.z);
                if (transform.position.y >= openHeight) {
                    transform.position = new Vector3(transform.position.x, openHeight, transform.position.z);
                    doorMoving = false;
                }
            } else {
                transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z);
                if (transform.position.y <= closedHeight) {
                    transform.position = new Vector3(transform.position.x, closedHeight, transform.position.z);
                    doorMoving = false;
                }
            }
        }
	}

    public void moveDoor() {
        doorMoving = true;
        doorOpen = !doorOpen;
    }
}
