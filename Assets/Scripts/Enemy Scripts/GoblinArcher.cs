using UnityEngine;
using RootMotion.FinalIK;

using soundEffect = SoundBank.SoundEffect;

public class GoblinArcher : MonoBehaviour {
	int health;
	Animator anim;
	float shootingRange = 70.0f;
	Transform player;
    Character character;
	bool playerClose;
    float arrowSpeed = 50.0f, timeBetweenShots = 3.0f, count = 0.0f;

    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    SoundBank soundBank;

    AimIK aimIK;

    // Use this for initialization
    void Start () {
		health = 30;
		playerClose = false;

		anim = gameObject.GetComponent<Animator>();

		player = GameObject.FindGameObjectWithTag("player").transform;
        character = player.gameObject.GetComponent<Character>();
        soundBank = GameObject.FindGameObjectWithTag("SoundBank").GetComponent<SoundBank>();

        aimIK = gameObject.GetComponent<AimIK>();
        aimIK.solver.IKPositionWeight = 0f;
    }
	
	// Update is called once per frame
	void Update () {
		float dist = Mathf.Abs(Vector3.Distance(player.position, transform.position));

		if (dist <= shootingRange) {
			playerClose = true;
		} else {
			playerClose = false;
		}
	}

    void FixedUpdate() {
        if (health > 0 && character.getAlive()) {
            if (playerClose) {
                RaycastHit hit;
                Vector3 centerBodyPlayer = new Vector3(player.position.x, player.position.y + 1.3f, player.position.z);
                Vector3 centerBody = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

                if (Physics.Raycast(centerBody, centerBodyPlayer - centerBody, out hit) && hit.transform.tag == "player") {
                    aimIK.solver.IKPositionWeight = 1.0f;

                    Vector3 targetDir = player.position - transform.position;
                    float step = 5.0f * Time.deltaTime;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), step);
                    transform.rotation = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f));

                    count += Time.deltaTime;
                    if (count >= timeBetweenShots && health > 0) {
                        count = 0;
                        GameObject temp = Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation) as GameObject;
                        temp.GetComponent<Rigidbody>().AddForce(arrowSpawn.transform.forward * arrowSpeed, ForceMode.Impulse);
                        temp.GetComponent<ArrowProjectile>().shooter = "enemy";
                        soundBank.playClip(soundEffect.SHOOT_ARROW);
                    }
                } else {
                    aimIK.solver.IKPositionWeight = 0f;
                    if (count > 0) {
                        count = 0;
                    }
                }
            } else {
                aimIK.solver.IKPositionWeight = 0f;
            }
        }
    }

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Arrow")) {
			if (other.GetComponent<ArrowProjectile>().shooter == "player" && health > 0) {
				anim.SetTrigger ("Hit1");
				takeDamage (10);
			}
			Destroy (other.gameObject);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.CompareTag ("Sword")) {
			Sword sword = other.GetComponent<Sword> ();
            if (sword.swinger == "player" && sword.swinging && !sword.hitOnce) {
                sword.hitOnce = true;
                if (sword.strongHit) {
                    takeDamage(60);
                } else {
                    takeDamage(30);
                }
            }
		}
	}

	void takeDamage(int damage) {
		health -= damage;
		if (health <= 0) {
			anim.SetTrigger ("Dead");
            aimIK.solver.IKPositionWeight = 0f;
            Invoke ("DeadGoblin", 5.0f);
		} else {
            soundBank.playClip(soundEffect.LIGHT_HIT);
        }
	}

	void DeadGoblin() {
		Destroy (gameObject);
	}
}