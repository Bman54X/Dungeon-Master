using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnToShop : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        SceneManager.LoadScene("Shop");
    }
}