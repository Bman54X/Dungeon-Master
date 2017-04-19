using UnityEngine;

public class CheckDeadEnemies : MonoBehaviour {
    public GameObject[] enemies;
    PlatformMover door;
    bool activated;
    public GameObject vrRoom, endRoom;

	// Use this for initialization
	void Start () {
        activated = false;
        door = gameObject.GetComponent<PlatformMover>();
        door.setActivation(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!activated) {
            foreach (GameObject enemy in enemies) {
                if (enemy != null) {
                    activated = false;
                    break;
                } else {
                    activated = true;
                }
            }

            if (activated) {
                door.setActivation(true);
                vrRoom.SetActive(true); endRoom.SetActive(true);
            }
        }
	}
}