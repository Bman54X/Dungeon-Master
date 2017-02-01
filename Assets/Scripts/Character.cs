using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    Vector3 moveDirection;
    public float speed = 6.0f, jumpSpeed = 50.0f,
				 rotateSpeed = 5.0f, gravity = 9.81f;
    CharacterController cc;
    Animator anim;
	bool waitToJump, jumpPressed, crouching, walking;
	bool startWalkCounter;
	int jumpCounter, walkCounter;

    // Use this for initialization
    void Start () {
        cc = GetComponent<CharacterController>();
        if (cc == null) {
            Debug.Log("No CharacterController found.");
        }

		anim = GetComponent<Animator>();
		if (anim == null) {
			Debug.Log("No Animator found.");
		}

		waitToJump = false; jumpPressed = false;
		crouching = false; walking = false;
		startWalkCounter = false;
		jumpCounter = 0; walkCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (cc.isGrounded && !jumpPressed) {
			if (Input.GetButtonDown ("NormalAttack")) {
				anim.SetTrigger ("NormalAttack");
			} else if (Input.GetButtonDown ("StrongAttack")) {
				anim.SetTrigger ("StrongAttack");
			}  else if (Input.GetButtonDown ("Stab")) {
				anim.SetTrigger ("Stab");
			}

			moveDirection = new Vector3 (0, 0, Input.GetAxis ("Vertical"));
			moveDirection = transform.TransformDirection (moveDirection);
            
			if (Input.GetButton ("Crouch") && !walking) {
				anim.SetBool ("CrouchHeld", true);
				crouching = true;
			} else if (Input.GetButton ("Walk") && !crouching) {
				anim.SetFloat ("Speed", 0.3f);
				walking = true;
			} else{
				anim.SetBool ("CrouchHeld", false);
				crouching = false; walking = false;
			}

			if (Input.GetButtonUp ("Walk")) {
				startWalkCounter = true;
			}

			if (crouching || walking) {
				moveDirection *= speed / 2.0f;
			} else {
				moveDirection *= speed;
			}

			//Key Press Stuff
			if (Input.GetButtonDown ("Jump")) {
				waitToJump = true;
				jumpPressed = true;
			}

			if (startWalkCounter) {
				walkCounter++;

				if (walkCounter >= 20) {
					walkCounter = 0;
					startWalkCounter = false;
				}
			}

			if (moveDirection.x == 0 && moveDirection.z == 0) {
				anim.SetFloat ("Speed", 0);
			} else if (Input.GetAxis("Vertical") < 0) {
				anim.SetFloat ("Speed", -1);
			} else if (!walking && !startWalkCounter) {
				anim.SetFloat("Speed", 1);
			}

			anim.SetBool("Jump", false);
		}

		if (waitToJump) {
			jumpCounter++;
			anim.SetBool ("Jump", true);
			if (jumpCounter == 20) {
				jumpCounter = 0;
				moveDirection.y = jumpSpeed;
				jumpPressed = false; waitToJump = false;
			}
		}

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);
    }
}