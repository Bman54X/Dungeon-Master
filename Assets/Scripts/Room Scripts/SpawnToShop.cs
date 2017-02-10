using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnToShop : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if(gameObject.tag == "player")
        SceneManager.LoadScene("Shop");
    }
}