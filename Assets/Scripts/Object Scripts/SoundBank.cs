using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour {
	public enum SoundEffect { SWORD_SWING, SWITCH_WEAPON, SHOOT_ARROW, GOLD };
	[SerializeField]
	private AudioClip footsteps, swordSwing, switchWeapon, shootArrow, gold;

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
		clipSource.volume = 0.25f;
	}

	// Use this for initialization
	void Start () {
		clips.Add (SoundEffect.SWORD_SWING, swordSwing);
		clips.Add (SoundEffect.SWITCH_WEAPON, switchWeapon);
		clips.Add (SoundEffect.SHOOT_ARROW, shootArrow);
		clips.Add (SoundEffect.GOLD, gold);
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