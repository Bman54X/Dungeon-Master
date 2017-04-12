using UnityEngine;

public class ActivateDeathTrigger : MonoBehaviour {
    public GameObject deathTrigger;

    void Start() {
        deathTrigger.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("player")) {
            deathTrigger.SetActive(true);
        }
    }
}