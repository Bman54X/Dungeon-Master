using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
	Character player;
	bool _swinging;
	bool _hitOnce;

	// Use this for initialization
	void Start () {
		swinging = false;
		player = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!_swinging) {
			_hitOnce = false;
		}
	}

	public bool swinging {
		get { return _swinging; }
		set { _swinging = value;}
	}

	public bool hitOnce {
		get { return _hitOnce; }
		set { _hitOnce = value;}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Switch" && _swinging && !hitOnce) {
			_hitOnce = true;
			other.gameObject.GetComponent<Switch>().switchActivate();
		}
	}
}