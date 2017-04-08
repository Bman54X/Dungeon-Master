using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour {
	public enum SoundEffect { SWORD_SWING, HEAVY_SWORD_SWING, SWITCH_WEAPON, SHOOT_ARROW, GOLD,
							  DRINK_POTION, HEALTH_VIAL, LIGHT_HIT, STRONG_HIT};
	[SerializeField]
	private AudioClip footsteps, swordSwing, heavySwordSwing, switchWeapon, shootArrow, gold,
					  drinkPotion, healthVial, lightHit, strongHit;

	Dictionary<SoundEffect, AudioClip> clips = new Dictionary<SoundEffect, AudioClip>();

	private AudioSource footstepSource, clipSource;

	void Awake() {
		footstepSource = gameObject.AddComponent<AudioSource> ();
		footstepSource.loop = true;
		footstepSource.playOnAwake = false;
		footstepSource.clip = footsteps;
		footstepSource.pitch = 0.9f;
		footstepSource.volume = 0.5f;

		clipSource = gameObject.AddComponent<AudioSource> ();
		clipSource.loop = false;
		clipSource.playOnAwake = false;
		clipSource.volume = 0.2f;
	}

	// Use this for initialization
	void Start () {
		clips.Add (SoundEffect.SWORD_SWING, swordSwing);
		clips.Add (SoundEffect.HEAVY_SWORD_SWING, heavySwordSwing);
		clips.Add (SoundEffect.SWITCH_WEAPON, switchWeapon);
		clips.Add (SoundEffect.SHOOT_ARROW, shootArrow);
		clips.Add (SoundEffect.GOLD, gold);
		clips.Add (SoundEffect.DRINK_POTION, drinkPotion);
		clips.Add (SoundEffect.HEALTH_VIAL, healthVial);
		clips.Add (SoundEffect.LIGHT_HIT, lightHit);
		clips.Add (SoundEffect.STRONG_HIT, strongHit);
	}

	public void playClip(SoundEffect sound) {
		clipSource.PlayOneShot(clips[sound], 1.0f);
	}

	public void footstepSounds(bool playing) {
		if (playing && !footstepSource.isPlaying) {
			footstepSource.Play ();
		} else if (!playing && footstepSource.isPlaying) {
			footstepSource.Stop ();
		}
	}
}