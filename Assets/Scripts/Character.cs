using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
	CharacterController cc;
	Animator anim;
	public Transform cameraTransform;

    Vector3 moveDirection;
	Vector3 prevLoc, curLoc, forward, right;

	bool waitToJump = false, jumpPressed = false, crouching = false, walking = false;
	bool startWalkCounter = false;

	int jumpCounter = 0, walkCounter = 0, holdAttack = 0;

	const float maxHealth = 100;
	public float speed = 6.0f, jumpSpeed = 50.0f,
	rotateSpeed = 5.0f, gravity = 9.81f;
	float gold = 0.0f, _health;

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

		curLoc = transform.position;
		_health = maxHealth;
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
			} else if (Input.GetButton ("Walk") && !crouching) {
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
			float dist = Mathf.Abs(Vector3.Distance (prevLoc, curLoc));

			if (dist >= 0.01f) {
				float xx = transform.rotation.x;
				float zz = transform.rotation.z;
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation (transform.position - prevLoc), Time.fixedDeltaTime * rotateSpeed);
				transform.rotation = new Quaternion (xx, transform.rotation.y, zz, transform.rotation.w);
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
			
        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);
    }

	public float health {
		get { return _health; }
		set { _health = value; }
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Health")) {
			if (health < maxHealth) {
				health += 10;
				if (health > maxHealth) {
					health = maxHealth;
				}
			}

			Destroy (other.gameObject);
		} else if (other.gameObject.CompareTag("Gold")) {
			gold += Random.Range (1, 21);

			Destroy (other.gameObject);
		}
	}	
}