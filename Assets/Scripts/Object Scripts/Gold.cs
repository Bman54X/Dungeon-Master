using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {
	bool _pickedUp = false;

	public bool pickedUp {
		get { return _pickedUp; }
		set { _pickedUp = value; }
	}
}