using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
	Character player;
	bool _swinging;

	// Use this for initialization
	void Start () {
		swinging = false;
		player = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public bool swinging {
		get { return _swinging; }
		set { _swinging = value; }
	}

	void OnTriggerEnter(Collider other) {
	}
}