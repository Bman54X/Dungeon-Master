using UnityEngine;

public class DoorTrigger : MonoBehaviour {
    public DoorClose door;

    void OnTriggerEnter(Collider other) {
        if (door != null) {
            door.moveDoor();
        }

        Destroy(gameObject);
    }
}