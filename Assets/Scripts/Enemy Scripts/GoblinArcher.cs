using UnityEngine;

using soundEffect = SoundBank.SoundEffect;

public class GoblinArcher : MonoBehaviour {
	int health;
	Animator anim;
	float shootingRange = 50.0f;
	Transform player;
	bool playerClose;
    float arrowSpeed = 40.0f, timeBetweenShots = 3.0f, count = 0.0f;

    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    SoundBank soundBank;

	// Use this for initialization
	void Start () {
		health = 30;
		playerClose = false;
		anim = gameObject.GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("player").transform;
        soundBank = GameObject.FindGameObjectWithTag("SoundBank").GetComponent<SoundBank>();
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
        if (playerClose) {
            Vector3 targetDir = player.position - transform.position;
            float step = 4.0f * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

            count += Time.deltaTime;
            if (count >= timeBetweenShots && health > 0) {
                count = 0;
                GameObject temp = Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation) as GameObject;
                temp.GetComponent<Rigidbody>().AddForce(arrowSpawn.transform.forward * arrowSpeed, ForceMode.Impulse);
                temp.GetComponent<ArrowProjectile>().shooter = "enemy";
                soundBank.playClip(soundEffect.SHOOT_ARROW);
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
			if (sword.swinging && !sword.hitOnce) {
				sword.hitOnce = true;
				takeDamage (30);
			}
		}
	}

	void takeDamage(int damage) {
		health -= damage;
		if (health <= 0) {
			anim.SetTrigger ("Dead");
			Invoke ("DeadGoblin", 5.0f);
		} else {
            soundBank.playClip(soundEffect.LIGHT_HIT);
        }
	}

	void DeadGoblin() {
		Destroy (gameObject);
	}
}