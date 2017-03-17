using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	string _tag;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 4.0f);
	}

	public string tag {
		get { return _tag; }
		set { _tag = value; }
	}
}