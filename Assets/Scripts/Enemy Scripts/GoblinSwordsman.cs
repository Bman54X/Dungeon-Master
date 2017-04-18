using UnityEngine;

using soundEffect = SoundBank.SoundEffect;

public class GoblinSwordsman : MonoBehaviour {
    int health;
    Animator anim;
    float searchRange = 10.0f, attackRange = 1.2f, initialYPosition;
    float attackLength = 1.0f, count = 0.0f, movementSpeed = 3.0f;
    Vector3 wanderTarget;
    Transform player;
    Character character;
    bool wandering, damageTaken;

    public Sword sword;

    SoundBank soundBank;

    // Use this for initialization
    void Start() {
        health = 60;
        initialYPosition = transform.position.y;
        wandering = false; damageTaken = false;
        wanderTarget = Vector3.zero;
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("player").transform;
        character = player.gameObject.GetComponent<Character>();
        soundBank = GameObject.FindGameObjectWithTag("SoundBank").GetComponent<SoundBank>();
        sword.swinger = "goblin";
        anim.SetFloat("Speed", 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (health > 0 && character.getAlive()) {
            float dist = Mathf.Abs(Vector3.Distance(player.position, transform.position));
            Vector3 targetDir = player.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);
            RaycastHit hit;

            Vector3 centerBody = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            Vector3 centerBodyPlayer = new Vector3(player.position.x, player.position.y + 1.1f, player.position.z);

            if ((Physics.Raycast(centerBody, centerBodyPlayer - centerBody, out hit) && hit.transform.tag == "player") || damageTaken) {
                wandering = false;
                if ((dist <= searchRange && angle < 50) || damageTaken) {
                    wandering = false;
                    anim.SetFloat("Speed", 1);

                    float step = 8.0f * Time.deltaTime;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), step);
                    transform.rotation = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f));

                    if (dist <= attackRange) {
                        anim.SetFloat("Speed", 0.5f);

                        if (!sword.swinging) {
                            count += Time.deltaTime;

                            if (count >= attackLength) {
                                anim.SetTrigger("Attack1");
                                Invoke("setSwinging", 0.3f);
                                count = 0;
                                Invoke("resetAttack", attackLength);
                            }
                        }
                    } else {
                        transform.Translate(0.0f, 0.0f, movementSpeed * Time.deltaTime);
                    }
                } else {
                    wandering = true;
                }
            } else {
                wandering = true;
            }

            if (wandering) {
                Wander();
            } else if (wanderTarget != Vector3.zero) {
                wanderTarget = Vector3.zero;
            }
        }
    }

    void Wander() {
        float dist = Mathf.Abs(Vector3.Distance(wanderTarget, transform.position));
        if (dist >= 0.05f) {
            if (wanderTarget == Vector3.zero) {
                RaycastHit hit;
                Vector3 temp, centerBody = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
                float rayDist;

                do {
                    temp = transform.position + Random.insideUnitSphere * 5.0f;
                    wanderTarget = new Vector3(temp.x, initialYPosition, temp.z);
                    rayDist = Mathf.Abs(Vector3.Distance(wanderTarget, centerBody));
                } while (Physics.Raycast(centerBody, wanderTarget - centerBody, out hit, rayDist));
            } else {
                Vector3 targetDir = wanderTarget - transform.position;
                float step = 6.0f * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), step);
                transform.rotation = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f));
                transform.Translate(0.0f, 0.0f, movementSpeed / 3.0f * Time.deltaTime);

                anim.SetFloat("Speed", 0.2f);
            }
        } else {
            wanderTarget = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Arrow")) {
            if (other.GetComponent<ArrowProjectile>().shooter == "player" && health > 0) {
                anim.SetTrigger("ArrowHit");
                Invoke("resetDamageTaken", 3.0f);
                takeDamage(20);
            }
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.CompareTag("Sword")) {
            Sword sword = other.GetComponent<Sword>();
            if (sword.swinger == "player" && sword.swinging && !sword.hitOnce) {
                sword.hitOnce = true;
                Invoke("resetDamageTaken", 3.0f);
                anim.SetTrigger("SwordHit");
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
            anim.SetTrigger("Dead");
            anim.SetFloat("Speed", 0);
            Invoke("DeadGoblin", 5.0f);
        } else {
            damageTaken = true;
            if (damage < 20) {
                soundBank.playClip(soundEffect.LIGHT_HIT);
            } else {
                soundBank.playClip(soundEffect.STRONG_HIT);
            }
        }
    }

    void resetDamageTaken() {
        damageTaken = false;
    }

    void setSwinging() {
        sword.swinging = true;
    }

    void resetAttack() {
        sword.swinging = false;
        sword.hitOnce = false;
        count = 0;
    }

    void DeadGoblin() {
        Destroy(gameObject);
    }
}