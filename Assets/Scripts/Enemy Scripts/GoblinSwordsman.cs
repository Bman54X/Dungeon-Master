using UnityEngine;

using soundEffect = SoundBank.SoundEffect;

public class GoblinSwordsman : MonoBehaviour {
    int health;
    Animator anim;
    float attackingRange = 50.0f;
    float attackLength = 1.0f, count = 0.0f;
    Transform player;

    public Sword sword;

    SoundBank soundBank;

    // Use this for initialization
    void Start() {
        health = 60;
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("player").transform;
        soundBank = GameObject.FindGameObjectWithTag("SoundBank").GetComponent<SoundBank>();
        sword.swinger = "goblin";
        anim.SetFloat("Speed", 0.0f);
    }

    // Update is called once per frame
    void Update() {
        float dist = Mathf.Abs(Vector3.Distance(player.position, transform.position));

        if (dist <= attackingRange) {
            if (!sword.swinging) {
                count += Time.deltaTime;

                if (count >= attackLength) {
                    anim.SetTrigger("Attack1");
                    sword.swinging = true;
                    count = 0;
                    Invoke("resetAttack", attackLength);
                }
            }
        }
    }

    void FixedUpdate() {
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Arrow")) {
            if (other.GetComponent<ArrowProjectile>().shooter == "player" && health > 0) {
                anim.SetTrigger("ArrowHit");
                takeDamage(10);
            }
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.CompareTag("Sword")) {
            Sword sword = other.GetComponent<Sword>();
            if (sword.swinger == "player" && sword.swinging && !sword.hitOnce) {
                sword.hitOnce = true;
                anim.SetTrigger("SwordHit");
                takeDamage(30);
            }
        }
    }

    void takeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            anim.SetTrigger("Dead");
            Invoke("DeadGoblin", 5.0f);
        } else {
            if (damage < 20) {
                soundBank.playClip(soundEffect.LIGHT_HIT);
            } else {
                soundBank.playClip(soundEffect.STRONG_HIT);
            }
        }
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