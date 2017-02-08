using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
	public enum Potions { SPEED, HEALTH, DEFENSE, GOLD }

	CharacterController cc;
	Animator anim;
	public Transform cameraTransform;

    Vector3 moveDirection;
	Vector3 prevLoc, curLoc, forward, right;

	bool waitToJump = false, jumpPressed = false, crouching = false, walking = false;
	bool startWalkCounter = false, crossbowFound = false, swordEquipped = true;
	bool changedPotions;

	const int maxHealth = 100, maxArrows = 30;
	int jumpCounter = 0, walkCounter = 0, holdAttack = 0, potionCount = 0;
	int bowAmmo = 0, bombAmmo = 0, gold = 0, _health;

	const float gravity = 9.81f;
	float speed = 6.0f, jumpSpeed = 15.0f, rotateSpeed = 10.0f;

	public Text healthText, goldText, addedGold, bowAmmoText;
	public Slider healthSlider;

	public GameObject backCrossbow, handCrossbow, backSword, handSword;
	public GameObject swordIcon, bowIcon;

	Potions potionEquipped = Potions.HEALTH;

	Dictionary<Potions, int> potionInventory = new Dictionary<Potions, int>();

    // Use this for initialization
    void Awake () {
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

		healthSlider.value = _health;
		healthText.text = _health.ToString();
		goldText.text = "Gold: " + gold;
		addedGold.text = "";
		bowAmmoText.text = bowAmmo.ToString();

		crossbowFound = true;

		backCrossbow.SetActive (true); handCrossbow.SetActive (false);
		backSword.SetActive (false); handSword.SetActive (true);
		bowIcon.SetActive (false); swordIcon.SetActive (true);

		potionInventory.Add (Potions.HEALTH, 0); potionInventory.Add (Potions.GOLD, 0);
		potionInventory.Add (Potions.DEFENSE, 0); potionInventory.Add (Potions.SPEED, 0);
	}
	
	// Update is called once per frame
	void Update () {
		healthSlider.value = _health;
		healthText.text = _health.ToString();
		goldText.text = "Gold: " + gold;
		bowAmmoText.text = bowAmmo.ToString();

		if (cc.isGrounded && !jumpPressed) {
			if (Input.GetButton ("NormalAttack") && swordEquipped) {
				holdAttack++;
			} else if (Input.GetButtonUp ("NormalAttack") && swordEquipped) {
				if (holdAttack >= 45) {
					anim.SetTrigger ("StrongAttack");
				} else {
					anim.SetTrigger ("NormalAttack");
				}
				holdAttack = 0;
			} else if (Input.GetButtonDown ("Stab") && swordEquipped) {
				anim.SetTrigger ("Stab");
			} else if (Input.GetButtonDown ("SwitchWeapon") && crossbowFound) {
				anim.SetTrigger ("GetSword");
				Invoke("switchWeapons", 0.4f);
			}

			if (potionCount > 0 && potionCount < 5) {
				potionCount++;
			} else if (potionCount >= 5) {
				
				potionCount = 0;
			}
			changedPotions = false;
			if (Input.GetButtonDown("HealthPotion")) {
				potionEquipped = Potions.HEALTH;
				changedPotions = true; potionCount++;
			} else if (Input.GetButtonDown("DefensePotion")) {
				potionEquipped = Potions.DEFENSE;
				changedPotions = true; potionCount++;
			} else if (Input.GetButtonDown("SpeedPotion")) {
				potionEquipped = Potions.SPEED;
				changedPotions = true; potionCount++;
			} else if (Input.GetButtonDown("GoldPotion")) {
				potionEquipped = Potions.GOLD;
				changedPotions = true; potionCount++;
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

	public int health {
		get { return _health; }
		set { _health = value; }
	}

	public bool getChangedPotions() {
		return changedPotions;
	}

	public Potions getPotion() {
		return potionEquipped;
	}

	public int getPotionAmount() {
		return potionInventory[potionEquipped];
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
			int chance = Random.Range (1, 11);
			if (chance <= 8) {
				gold += Random.Range (5, 51);
			} else {
				gold += Random.Range (51, 100);
			}
			addedGold.text = "+" + gold.ToString ();
			Invoke("resetGoldText", 1.5f);

			Destroy (other.gameObject);
		}  else if (other.gameObject.CompareTag("ArrowBundle")) {
			if (bowAmmo < maxArrows) {
				bowAmmo += 5;
				if (bowAmmo > maxArrows) {
					bowAmmo = maxArrows;
				}
			}

			Destroy (other.gameObject);
		}
	}

	void resetGoldText() {
		addedGold.text = "";
	}

	void switchWeapons() {
		if (swordEquipped) {
			backCrossbow.SetActive (false); handCrossbow.SetActive (true);
			backSword.SetActive (true); handSword.SetActive (false);
			bowIcon.SetActive (true); swordIcon.SetActive (false);
		} else {
			backCrossbow.SetActive (true); handCrossbow.SetActive (false);
			backSword.SetActive (false); handSword.SetActive (true);
			bowIcon.SetActive (false); swordIcon.SetActive (true);
		}

		swordEquipped = !swordEquipped;
	}
}