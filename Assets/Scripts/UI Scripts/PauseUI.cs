using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour {
	Character player;
	MouseOrbit mouse;
	public MouseLook crossbowYCamera;
	MouseLook crossbowXCamera;
	public GameObject pausePanel;
	bool paused = false;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
		crossbowXCamera = GameObject.FindGameObjectWithTag ("player").GetComponent<MouseLook> ();
		mouse = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseOrbit>();
		pausePanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause")) {
			paused = !paused;
			player.setPaused (paused);
			mouse.setPaused (paused);
			crossbowXCamera.changeCanLook (!paused);
			crossbowYCamera.changeCanLook (!paused);

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