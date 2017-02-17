using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour {
	Character player;
	MouseOrbit mouse;
	public GameObject pausePanel;
	bool paused = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
		mouse = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseOrbit>();
		pausePanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause")) {
			paused = !paused;
			player.setPaused (paused);
			mouse.setPaused (paused);

			if (paused) {
				Time.timeScale = 0.0f;
				pausePanel.SetActive (true);
			} else {
				Time.timeScale = 1.0f;
				pausePanel.SetActive (false);
			}
		}
	}
}