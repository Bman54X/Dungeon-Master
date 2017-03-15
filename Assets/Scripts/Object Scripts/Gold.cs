using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {
	bool _pickedUp;
	int value;

	void Start() {
		_pickedUp = false;
			
		int chance = Random.Range (1, 11);

		if (chance <= 8) {
			value = Random.Range (5, 51);
		} else {
			value = Random.Range (51, 100);
		}
	}

	public bool pickedUp {
		get { return _pickedUp; }
		set { _pickedUp = value; }
	}

	public int getValue() {
		return value;
	}
}