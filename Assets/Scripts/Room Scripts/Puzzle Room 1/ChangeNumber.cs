using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNumber : MonoBehaviour {
    int i = 0;
	bool hit = false;

    void changeNumber() {
        GetComponent<TextMesh>().text = i.ToString();
        i++;
        if (i == 11) {
            i = 0;
            GetComponent<TextMesh>().text = i.ToString();
        }
		hit = true;

		Invoke("resetHit", Time.deltaTime);
    }

    void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "player" && Input.GetButtonUp("Interact") && !hit) {
			changeNumber ();
        }
    }

	void resetHit() {
		hit = false;
	}
}