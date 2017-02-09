using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
	public enum Potions { GOFOR, HEALTH, DEFENSE, SPEED, GOLD, GOBACK }

	CharacterController cc;
	Animator anim;
	public Transform cameraTransform;

    Vector3 moveDirection;
	Vector3 prevLoc, curLoc, forward, right;

	bool waitToJump = false, jumpPressed = false, crouching = false, walking = false;
	bool startWalkCounter = false, crossbowFound = false, swordEquipped = true;
	bool changedPotions = false, potionActivated = false, potionUsed = false;

	const int maxHealth = 100, maxArrows = 30;
	int jumpCounter = 0, walkCounter = 0, holdAttack = 0;
	int bowAmmo = 0, bombAmmo = 0, gold = 0, _health;
	int speedMultiplier = 1, goldMultiplier = 1;

	const float gravity = 9.81f;
	float speed = 6.0f, jumpSpeed = 15.0f, rotateSpeed = 10.0f;
	float potionTimer = 15.0f;

	public Text healthText, goldText, addedGold, bowAmmoText, countdownText;
	public Slider healthSlider;

	public GameObject backCrossbow, handCrossbow, backSword, handSword;
	public GameObject swordIcon, bowIcon;

	Potions potionEquipped = Potions.HEALTH;

	Dictionary<Potions, int> potionInventory = new Dictionary<Potions, int>();
	private Dictionary<Potions, System.Action> potionEffect = new Dictionary<Potions, System.Action>();

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
		countdownText.text = "";
		bowAmmoText.text = bowAmmo.ToString();

		crossbowFound = true;

		backCrossbow.SetActive (true); handCrossbow.SetActive (false);
		backSword.SetActive (false); handSword.SetActive (true);
		bowIcon.SetActive (false); swordIcon.SetActive (true);

		potionInventory.Add (Potions.HEALTH, 4); potionInventory.Add (Potions.GOLD, 2);
		potionInventory.Add (Potions.DEFENSE, 5); potionInventory.Add (Potions.SPEED, 3);

		potionEffect.Add (Potions.HEALTH, giveHealth); potionEffect.Add (Potions.GOLD, moreGold);
		potionEffect.Add (Potions.DEFENSE, doubleDefense); potionEffect.Add (Potions.SPEED, fastSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		healthSlider.value = _health;
		healthText.text = _health.ToString();
		goldText.text = "Gold: " + gold;
		bowAmmoText.text = bowAmmo.ToString();

		if (cc.isGrounded && !jumpPressed) {
			checkButtonPresses();

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
				moveDirection *= (speed / 2.0f) * speedMultiplier;
			} else {
				moveDirection *= speed * speedMultiplier;
			}

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

		if (potionActivated) {
			potionTimer -= Time.deltaTime;
			int g = (int)potionTimer;
			countdownText.text = g.ToString();

			if (potionTimer <= 0.0f) {
				potionActivated = false;
				potionTimer = 15.0f;

				speedMultiplier = 1; goldMultiplier = 1;
				countdownText.text = "";
			}
		}
    }

	void checkButtonPresses() {
		//Checking attack
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

		//Switching Potions on keyboard
		changedPotions = false;
		if (Input.GetButtonDown("HealthPotion")) {
			potionEquipped = Potions.HEALTH;
			changedPotions = true;
		} else if (Input.GetButtonDown("DefensePotion")) {
			potionEquipped = Potions.DEFENSE;
			changedPotions = true;
		} else if (Input.GetButtonDown("SpeedPotion")) {
			potionEquipped = Potions.SPEED;
			changedPotions = true;
		} else if (Input.GetButtonDown("GoldPotion")) {
			potionEquipped = Potions.GOLD;
			changedPotions = true;
		}

		//Switching potions on joystick
		if (DPadButtons.right) {
			potionEquipped++;
			if (potionEquipped == Potions.GOBACK) {
				potionEquipped = Potions.HEALTH;
			}
			changedPotions = true;
		} else if (DPadButtons.left) {
			potionEquipped--;
			if (potionEquipped == Potions.GOFOR) {
				potionEquipped = Potions.GOLD;
			}
			changedPotions = true;
		}

		potionUsed = false;
		//Checking if using potion
		if (Input.GetButtonDown("DrinkPotion") && !potionActivated && potionInventory [potionEquipped] > 0) {
			if (potionEquipped != Potions.HEALTH) {
				potionActivated = true;
			}

			potionUsed = true;
			potionInventory[potionEquipped]--;
			potionEffect[potionEquipped]();
		}
	}

	public int health {
		get { return _health; }
		set { _health = value; }
	}

	public bool getChangedPotions() {
		return changedPotions;
	}

	public bool getPotionUsed() {
		return potionUsed;
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
			gold *= goldMultiplier;
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

	void giveHealth() {
		health += 50;
		if (health > maxHealth) {
			health = maxHealth;
		}
	}

	void fastSpeed() {
		speedMultiplier = 2;
	}

	void doubleDefense() {
		
	}

	void moreGold() {
		goldMultiplier = 2;
	}
}