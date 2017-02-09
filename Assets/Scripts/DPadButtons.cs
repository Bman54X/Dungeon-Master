using UnityEngine;
using System.Collections;

public class DPadButtons : MonoBehaviour {
	public static bool up;
	public static bool down;
	public static bool left;
	public static bool right;

	float lastX;
	float lastY;

	void Start() {
		up = down = left = right = false;
		lastX = Input.GetAxis("ChangePotion");
		//lastY = Input.GetAxis("DpadY");
	}

	void Update() {
		if (Input.GetAxis ("ChangePotion") == 1 && lastX != 1) { 
			right = true; lastX = 1;
		} else { 
			right = false; 
		}

		if (Input.GetAxis ("ChangePotion") == -1 && lastX != -1) { 
			left = true; lastX = -1;
		} else { 
			left = false; 
		}

		if (Input.GetAxis ("ChangePotion") == 0) {
			right = false; left = false; lastX = 0;
		}
		//if(Input.GetAxis ("DPadY") == 1 && lastDpadY != 1) { up = true; } else { up = false; }
		//if(Input.GetAxis ("DPadY") == -1 && lastDpadY != -1) { down = true; } else { down = false; }
	}
}