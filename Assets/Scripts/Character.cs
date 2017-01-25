using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    Vector3 moveDirection;
    public float speed = 6.0f, jumpSpeed = 15.0f,
                 rotateSpeed = 5.0f, gravity = 9.81f;
    CharacterController cc;

    // Use this for initialization
    void Start () {
        cc = GetComponent<CharacterController>();
        if (cc == null) {
            Debug.Log("No CharacterController found.");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (cc.isGrounded) {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
            moveDirection *= speed;

            //Key Press Stuff
            if (Input.GetButtonDown("Jump")) {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);

        /*float y = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 7.0f;

        transform.Rotate(0, y, 0);
        transform.Translate(0, moveDirection.y, z);*/
    }
}
