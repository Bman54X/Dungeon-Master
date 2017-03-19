using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCode : MonoBehaviour {
    public GameObject N1, N2, N3;
	public GameObject crossbowTutorial, switchTutorial;
	public Text acquireCrossbow;
	public Transform pedestal, pedestalPosition;
	int speed = 2;

	bool correct = false;

	void Start() {
		crossbowTutorial.SetActive (false); switchTutorial.SetActive (false);
		acquireCrossbow.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (!correct) {
			checkCode ();
		} else {
			pedestal.position = Vector3.MoveTowards (pedestal.position, pedestalPosition.position, speed * Time.deltaTime);
		}
	}

    void checkCode() {
        if (N1 != null) {
            if (N1.GetComponentInChildren<TextMesh>().text == 9.ToString() && 
				N2.GetComponentInChildren<TextMesh>().text == 7.ToString() && 
				N3.GetComponentInChildren<TextMesh>().text == 2.ToString()) {
				correct = true;
				crossbowTutorial.SetActive (true); switchTutorial.SetActive (true);
				Invoke ("resetCrossbowText", 3.0f);
				acquireCrossbow.text = "You acquired the crossbow!";
            }
        }
    }

	void resetCrossbowText() {
		acquireCrossbow.text = "";
	}
}