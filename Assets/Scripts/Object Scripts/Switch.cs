using UnityEngine;

public class Switch : MonoBehaviour {
	MeshRenderer mesh;
	bool activated;
	public PlatformMover platform;
    public GameObject vrPlayer;
    private MoveIfDead mid;

	// Use this for initialization
	void Start () {
        mid = vrPlayer.gameObject.GetComponent<MoveIfDead>();
        activated = false;
		mesh = GetComponent<MeshRenderer>();
		mesh.material.color = Color.red;
		platform.setActivation (false);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Arrow") || other.CompareTag("vrRock")) {
			switchActivate();
            mid.crossbowVRpr = true;
			Destroy (other.gameObject);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.CompareTag ("Sword")) {
			Sword sword = other.GetComponent<Sword> ();
			if (sword.swinging && !sword.hitOnce) {
				sword.hitOnce = true;
				switchActivate();
			}
		}
	}

	public void switchActivate() {
		if (!activated) {
			mesh.material.color = Color.green;
		} else {
			mesh.material.color = Color.red;
		}
		activated = !activated;
		platform.setActivation (activated);
	}
}