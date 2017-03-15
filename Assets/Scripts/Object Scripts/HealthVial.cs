using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthVial : MonoBehaviour {
	int value;
	bool _pickedUp;

	// Use this for initialization
	void Start () {
		value = 10;
		_pickedUp = false;
	}

	public bool pickedUp {
		get { return _pickedUp; }
		set { _pickedUp = value; }
	}
	
	public int getValue() {
		return value;
	}
}