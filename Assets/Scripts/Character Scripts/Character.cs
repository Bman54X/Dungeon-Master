using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using RootMotion.FinalIK;

using soundEffect = SoundBank.SoundEffect;

public class Character : MonoBehaviour {
	CharacterController cc;
	Animator anim;
	public Transform arrowSpawn;
	Transform cameraTransform, spawnPoint;
	public Transform startingPoint;

    private MoveIfDead mid;
    public GameObject vrPlayer;
    AimIK aimIK;

    Vector3 moveDirection;
	Vector3 prevLoc, curLoc, forward, right;

	bool waitToJump = false, jumpPressed = false, crouching = false, walking = false;
	bool startWalkCounter = false, crossbowFound = false, swordEquipped = true;
	bool changedPotions = false, potionActivated = false, potionUsed = false;
	bool paused = false, alive = true, maxHealthAlready = false, aiming = false;
	bool attacking = false, checkAttack = false, actionInProgress = false;

	const int maxHealth = 100, maxArrows = 30;
	int bowAmmo = 10, gold = 0, _health;
	int speedMultiplier = 1, goldMultiplier = 1, defense = 1;

	const float gravity = 9.81f, attackTimer = 1.0f;
	float speed = 6.0f, rotateSpeed = 10.0f, arrowSpeed = 80.0f;
	float potionTimer = 15.0f, holdAttack = 0.0f, walkCounter = 0.0f, jumpCounter = 0.0f;
    float jumpSpeed = 18.55f;

    public Transform rangeSpawn, VRSpawn;

	public Text healthText, goldText, addedGold, bowAmmoText, countdownText, acquiredCrossbow;
	public Slider healthSlider;
	Slider attackSlider;

	public GameObject backCrossbow, handCrossbow, backSword, handSword;
	public GameObject swordIcon, bowIcon, attackSliderObject, arrowPrefab;
	public GameObject mainCamera, crossbowCamera;
	Sword sword;

	MouseLook mouseLook;
	SoundBank soundBank;

	List<Potion> potionInventory = new List<Potion>();
	int currentPotion;

    // Use this for initialization
    void Start () {
        mid = vrPlayer.GetComponentInChildren<MoveIfDead>();
        cc = GetComponent<CharacterController>();
        if (cc == null) {
            Debug.Log("No CharacterController found.");
        }

		anim = GetComponent<Animator>();
		if (anim == null) {
			Debug.Log("No Animator found.");
		}

		//Set default values
		curLoc = transform.position;
		_health = maxHealth;

		spawnPoint = startingPoint;
		transform.position = spawnPoint.position;

		healthSlider.value = _health;
		healthText.text = _health.ToString();
		goldText.text = "Gold: " + gold;
		countdownText.text = acquiredCrossbow.text = addedGold.text = "";
		bowAmmoText.text = bowAmmo.ToString();

		attackSlider = attackSliderObject.GetComponent<Slider> ();
		mouseLook = gameObject.GetComponent<MouseLook>();
		mouseLook.changeCanLook (false);
        soundBank = GameObject.FindGameObjectWithTag("SoundBank").GetComponent<SoundBank>();

		crossbowFound = true;

		//Show and hide various parts of the UI
		if (!crossbowFound) {
			backCrossbow.SetActive (false);
		}

		handCrossbow.SetActive (false);
		backSword.SetActive (false); handSword.SetActive (true);
		bowIcon.SetActive (false); swordIcon.SetActive (true);
		attackSliderObject.SetActive (false);

		sword = handSword.GetComponent<Sword>();

		aimIK = gameObject.GetComponent<AimIK>();
		aimIK.solver.IKPositionWeight = 0f;﻿

		cameraTransform = mainCamera.transform;
		crossbowCamera.SetActive (false);

		//Add all the available potions to the inventory
		potionInventory.Add(new HealthPotion());
		potionInventory.Add(new GoldPotion());
		potionInventory.Add(new DefensePotion());
		potionInventory.Add(new SpeedPotion());

		currentPotion = 0;
	}

	void Update() {
        if (Input.GetKeyDown(KeyCode.N))
        {
            transform.position = rangeSpawn.position;
            crossbowFound = true;
        } else if (Input.GetKeyDown(KeyCode.M))
        {
            transform.position = startingPoint.position;
            crossbowFound = false;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            transform.position = VRSpawn.position;
            mid.vrPlatR = true;
            crossbowFound = true;
        }
        if (checkAttack) {
			if (!anim.GetCurrentAnimatorStateInfo(1).IsName ("normalSlash") &&
				!anim.GetCurrentAnimatorStateInfo(1).IsName ("overheadAttack") &&
				!anim.GetCurrentAnimatorStateInfo(1).IsName ("quickStab")) {
				attacking = false;
				sword.swinging = false;
				checkAttack = false;
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Only check for input and update position when not paused
		if (!paused && alive) {
			checkMovement();
		}
    }

	void checkMovement() {
		//If on the ground and jump has not already been pressed
		if (cc.isGrounded && !jumpPressed) {
			//Check various inputs
			checkButtonPresses ();

			if (!aiming) {
				//Update movement based on player's input
				float x = Input.GetAxis ("Horizontal");
				float z = Input.GetAxis ("Vertical");

				//Check if the player is holding crouch or walk
				if (Input.GetButton ("Crouch") && !walking) {
					anim.SetBool ("CrouchHeld", true);
					crouching = true;
				} else if (Input.GetButton ("Walk") && !crouching) {
					anim.SetFloat ("Speed", 0.3f);
					walking = true;
				} else {
					anim.SetBool ("CrouchHeld", false);
					crouching = false;
					walking = false;
				}

				//Make the forward input be relative to the camera's forward
				forward = cameraTransform.TransformDirection (Vector3.forward);
				forward.y = 0;
				forward = forward.normalized;
				right = new Vector3 (forward.z, 0, -forward.x);

				//Update the move direction
				moveDirection = (x * right + z * forward).normalized;

				//Update rotation if need be
				prevLoc = curLoc;
				curLoc = transform.position;
				float distX = Mathf.Abs (prevLoc.x - curLoc.x);
				float distZ = Mathf.Abs (prevLoc.z - curLoc.z);

				if (distX >= 0.02f || distZ >= 0.02f) {
					float xx = transform.rotation.x;
					float zz = transform.rotation.z;
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (transform.position - prevLoc), Time.fixedDeltaTime * rotateSpeed);
					transform.rotation = new Quaternion (xx, transform.rotation.y, zz, transform.rotation.w);
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

				//Check if the walk button has been released
				if (Input.GetButtonUp ("Walk")) {
					startWalkCounter = true;
				}

				//Checks how long until the walk ends to update the animation with
				if (startWalkCounter) {
					walkCounter += Time.deltaTime;

					if (walkCounter >= 0.4f) {
						walkCounter = 0.0f;
						startWalkCounter = false;
					}
				}

				//Changed the current animation based on the movement speed
				if (x == 0 && z == 0) {
					soundBank.footstepSounds (false);
					anim.SetFloat ("Speed", 0);
				} else if (!walking && !startWalkCounter) {
					anim.SetFloat ("Speed", 1);
					soundBank.footstepSounds (true);
				}
			} else {
				moveDirection = new Vector3 (0, 0, 0);
				soundBank.footstepSounds (false);
			}
		} else if (!cc.isGrounded) {
			RaycastHit hitInfo = new RaycastHit();
			if (Physics.Raycast (new Ray (transform.position, Vector3.down), out hitInfo, 0.2f)) {
				cc.Move (hitInfo.point - transform.position);
			}

			if (!cc.isGrounded) {
				soundBank.footstepSounds (false);
			}
		}

		//If jump has been pressed, wait a bit to add jump velocity due to animation delay
		if (waitToJump) {
			jumpCounter += Time.deltaTime;
			anim.SetBool ("Jump", true);
			if (jumpCounter >= 0.45f) {
				jumpCounter = 0.0f;
				moveDirection.y = jumpSpeed;
                
				jumpPressed = false; waitToJump = false;
				anim.SetBool ("Jump", false);
			}
		}

		//Update the player's y velocity based on gravity
		moveDirection.y -= gravity * Time.deltaTime;

        //Move the character controller
        cc.Move (moveDirection * Time.deltaTime);

		//If the player has activated a potion, start the potion timer
		//and reset it and its effects when it reaches 0
		if (potionActivated) {
			potionTimer -= Time.deltaTime;
			int g = (int)potionTimer;
			countdownText.text = g.ToString ();

			if (potionTimer <= 0.0f) {
				potionActivated = false;
				potionTimer = 15.0f;
				countdownText.text = "";

				potionInventory [currentPotion].deactivate ();
			}
		}
	}

	//Check various inputs
	void checkButtonPresses() {
		//Checking attack
		if (Input.GetButton ("NormalAttack") && swordEquipped && !attacking) {
			holdAttack += Time.deltaTime;
			if (holdAttack >= 0.1f) {
				attackSliderObject.SetActive (true);
			}
			attackSlider.value = holdAttack;
		//When the attack button has been released
		} else if (Input.GetButtonUp ("NormalAttack") && swordEquipped && !attacking) {
			//Based on how long the button was held, attack with a weak or strong attack
			if (holdAttack >= attackTimer) {
				anim.SetTrigger ("StrongAttack");
				Invoke("strongHitSound", 0.6f);
				Invoke("setCheckAttack", 0.7f);
			} else {
				anim.SetTrigger ("NormalAttack");
				Invoke("setCheckAttack", 0.4f);
				soundBank.playClip (soundEffect.SWORD_SWING);
			}
			attacking = true;

			attackSliderObject.SetActive (false);
			holdAttack = 0.0f;
		//Check for stab input
		} else if (Input.GetButtonDown ("Stab") && swordEquipped && !attacking) {
			anim.SetTrigger ("Stab");
			Invoke("setCheckAttack", 0.5f);
			attacking = true;
		} else if (!swordEquipped && (Input.GetButton ("AimBow") || Input.GetAxis("AimBowJoystick") >= 0.5f)) {
			if (!aiming) {
				anim.SetBool ("Aiming", true);
				aimIK.solver.IKPositionWeight = 1f;
				Invoke ("setAnimSpeed", 0.4f);
				Invoke ("setCameras", 0.2f);
				aiming = true;
			}

			if (Input.GetButtonDown ("NormalAttack") && bowAmmo > 0 && !actionInProgress) {
				bowAmmo--;
				bowAmmoText.text = bowAmmo.ToString();
				actionInProgress = true;
				Invoke ("setActionInProgress", 1.0f);

				GameObject temp = Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation) as GameObject;
				temp.GetComponent<Rigidbody>().AddForce (arrowSpawn.transform.forward * arrowSpeed, ForceMode.Impulse);
				temp.GetComponent<ArrowProjectile>().shooter = "player";
				soundBank.playClip (soundEffect.SHOOT_ARROW);
			}
		//Check if the player wishes to switch weapons
		} else if (Input.GetButtonDown ("SwitchWeapon") && crossbowFound && !actionInProgress) {
			anim.SetTrigger ("GetSword");
			Invoke("switchWeapons", 0.4f);
			actionInProgress = true;
			Invoke ("setActionInProgress", 1.0f);
			soundBank.playClip (soundEffect.SWITCH_WEAPON);
		} else if (aiming && !swordEquipped && (Input.GetButtonUp("AimBow") || Input.GetAxis("AimBowJoystick") < 0.5f)) {
			aiming = false;
			crossbowCamera.SetActive (false); handCrossbow.SetActive (true);
			mainCamera.SetActive (true); mouseLook.changeCanLook(false);
			anim.SetBool ("Aiming", false);
			aimIK.solver.IKPositionWeight = 0f;﻿
			anim.speed = 1.0f;
		}

		//Switching potions on keyboard
		changedPotions = false;
		if (Input.GetButtonDown("HealthPotion")) {
			currentPotion = 0;
			changedPotions = true;
		} else if (Input.GetButtonDown("DefensePotion")) {
			currentPotion = 1;
			changedPotions = true;
		} else if (Input.GetButtonDown("SpeedPotion")) {
			currentPotion = 2;
			changedPotions = true;
		} else if (Input.GetButtonDown("GoldPotion")) {
			currentPotion = 3;
			changedPotions = true;
		}

		//Switching potions on joypad
		if (DPadButtons.right) {
			currentPotion++;
			if (currentPotion == 4) {
				currentPotion = 0;
			}
			changedPotions = true;
		} else if (DPadButtons.left) {
			currentPotion--;
			if (currentPotion == -1) {
				currentPotion = 3;
			}
			changedPotions = true;
		}

		potionUsed = false;
		maxHealthAlready = false;
		//Checking if using potion
		if (Input.GetButtonDown("DrinkPotion") && !potionActivated && potionInventory[currentPotion].getInventory() > 0) {
			bool check = potionInventory[currentPotion].potionEffect();

			if (!check) {
				maxHealthAlready = true;
			} else {
				potionInventory [currentPotion].changeInventory (-1);
				potionUsed = true;
				healthSlider.value = _health;
				healthText.text = _health.ToString();
				soundBank.playClip (soundEffect.DRINK_POTION);
			}
		}
	}

	//Update UI and placement of weapons on character
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

	//Reset the added gold text
	void resetGoldText() {
		addedGold.text = "";
	}
		
	void resetCrossbowText() {
		acquiredCrossbow.text = "";
	}

	void strongHitSound() {
		soundBank.playClip (soundEffect.HEAVY_SWORD_SWING);
	}

	void setAnimSpeed() {
		anim.speed = 0.0f;
	}

	void setCameras() {
		crossbowCamera.SetActive (true); mainCamera.SetActive (false);
		mouseLook.changeCanLook(true); handCrossbow.SetActive (false);
	}

	void setCheckAttack() {
		checkAttack = !checkAttack;
		sword.swinging = true;
	}

	void setActionInProgress() {
		actionInProgress = false;
	}

	public void takeDamage(int damage) {
		_health -= damage / defense;

		if (damage < 20) {
			anim.SetTrigger ("Hit1");
			soundBank.playClip (soundEffect.LIGHT_HIT);
		} else {
			anim.SetTrigger ("Hit2");
			soundBank.playClip (soundEffect.STRONG_HIT);
		}

		//Update health and various text
		healthSlider.value = _health;
		healthText.text = _health.ToString();

		if (_health <= 0) {
			_health = 0;

			//Update health and various text
			healthText.text = _health.ToString();

			playerDeath();
		}
	}

	void playerDeath() {
		anim.SetTrigger ("Dead");
		alive = false;
		Invoke ("respawn", 3.0f);
	}

	void respawn() {
		alive = true;
		anim.SetTrigger ("Alive");
		transform.position = spawnPoint.position;

		//Set deafult values
		curLoc = transform.position;
		_health = maxHealth;

		healthSlider.value = _health;
		healthText.text = _health.ToString();
		bowAmmoText.text = bowAmmo.ToString();
	}
		
	void OnTriggerEnter(Collider other) {
		//If the player has picked up a health vial,
		//add 50 health up to the maximum
		if (other.gameObject.CompareTag("Health")) {
			HealthVial thisVial = other.gameObject.GetComponent<HealthVial>();
			if (!thisVial.pickedUp) {
				if (health < maxHealth) {
					health += thisVial.getValue();
					if (health > maxHealth) {
						health = maxHealth;
					}
				}

				healthSlider.value = _health;
				healthText.text = _health.ToString();
				soundBank.playClip (soundEffect.HEALTH_VIAL);
				thisVial.pickedUp = true;

				Destroy (other.gameObject);
			}
		//If the player has picked up gold
		} else if (other.gameObject.CompareTag("Gold")) {
			Gold thisGold = other.gameObject.GetComponent<Gold>();
			if (!thisGold.pickedUp) {
				int addedGoldNum = thisGold.getValue() * goldMultiplier;

				//Update gold values
				gold += addedGoldNum;
				goldText.text = "Gold: " + gold;
				addedGold.text = "+" + addedGoldNum.ToString();
				Invoke ("resetGoldText", 1.5f);
				soundBank.playClip (soundEffect.GOLD);
				thisGold.pickedUp = true;

				Destroy (other.gameObject);
			}
		//If the player has picked up an arrow bundle,
		//add 5 up to the maximum
		} else if (other.gameObject.CompareTag("ArrowBundle")) {
			if (bowAmmo < maxArrows) {
				bowAmmo += 5;
				if (bowAmmo > maxArrows) {
					bowAmmo = maxArrows;
				}
			}
			bowAmmoText.text = bowAmmo.ToString();

			Destroy (other.gameObject);
		} else if (other.gameObject.CompareTag("Crossbow")) {
			crossbowFound = true;
			backCrossbow.SetActive (true);
			Invoke ("resetCrossbowText", 3.0f);
			acquiredCrossbow.text = "You acquired the crossbow!";

			Destroy (other.gameObject);
		}
	}

	//various getters and setters
	public int health {
		get { return _health; }
		set { _health = value; }
	}

	public int _maxHealth {
		get { return maxHealth; }
	}

	public int _speedMultiplier {
		set { speedMultiplier = value; }
	}

	public int _defense {
		set { defense = value; }
	}

	public int _goldMultiplier {
		set { goldMultiplier = value; }
	}

	public bool _potionActivated {
		set { potionActivated = value; }
	}

	public bool getChangedPotions() {
		return changedPotions;
	}

	public bool getPotionUsed() {
		return potionUsed;
	}

	public int getPotion() {
		return currentPotion;
	}

	public bool getAlive() {
		return alive;
	}

	public bool getMaxHealthAlready() {
		return maxHealthAlready;
	}

	public int getPotionAmount() {
		return potionInventory[currentPotion].getInventory();
	}

	public void setPaused(bool newPause) {
		paused = newPause;
	}

	public void setSpawnPoint(Transform spawn) {
		spawnPoint = spawn;
	}
}