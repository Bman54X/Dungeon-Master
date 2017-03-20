using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCode : MonoBehaviour {
    public GameObject N1, N2, N3;
	public GameObject crossbowTutorial, switchTutorial;
	public Transform pedestal, pedestalPosition;
	public TextMesh prompt;
	int speed = 2;

	bool correct = false;

	void Start() {
		crossbowTutorial.SetActive (false); switchTutorial.SetActive (false);
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
				prompt.text = "Correct! Collect your reward to the right!";
            }
        }
    }
}