using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
    Vector3 moveDirection;
    public float speed = 6.0f, jumpSpeed = 50.0f,
				 rotateSpeed = 5.0f, gravity = 9.81f;
    CharacterController cc;
    Animator anim;
	public Transform cameraTransform;
	bool waitToJump, jumpPressed, crouching, walking;
	bool startWalkCounter;
	int jumpCounter = 0, walkCounter = 0, holdAttack = 0;
	Vector3 prevLoc, curLoc, forward, right;

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

		curLoc = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (cc.isGrounded && !jumpPressed) {
			if (Input.GetButton ("NormalAttack")) {
				holdAttack++;
			} else if (Input.GetButtonUp ("NormalAttack")) {
				if (holdAttack >= 45) {
					anim.SetTrigger ("StrongAttack");
				} else {
					anim.SetTrigger ("NormalAttack");
				}
				holdAttack = 0;
			}  else if (Input.GetButtonDown ("Stab")) {
				anim.SetTrigger ("Stab");
			}

			float x = Input.GetAxis ("Horizontal");
			float z = Input.GetAxis ("Vertical");
            
			if (Input.GetButton ("Crouch") && !walking) {
				anim.SetBool ("CrouchHeld", true);
				crouching = true;
			} //else if ((Input.GetButton ("Walk") && !crouching) || (z > 0 && z < 0.5f)) {
			else if (Input.GetButton ("Walk") && !crouching) {
				anim.SetFloat ("Speed", 0.3f);
				walking = true;
			} else {
				anim.SetBool ("CrouchHeld", false);
				crouching = false; walking = false;
			}

			forward = cameraTransform.TransformDirection(Vector3.forward);
     		forward.y = 0;
     		forward = forward.normalized;
     		right = new Vector3(forward.z, 0, -forward.x);

     		moveDirection = (x * right  + z * forward).normalized;

			prevLoc = curLoc;
			curLoc = transform.position;

			if (prevLoc != curLoc) {
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation (transform.position - prevLoc), Time.fixedDeltaTime * rotateSpeed);
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
			if (Input.GetButtonDown ("Jump") && !crouching) {
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
			
        //transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);
    }
}