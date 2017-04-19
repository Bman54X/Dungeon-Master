using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {
    public GameObject meleeRoom, vrRoom, endRoom;
    public Transform character, meleeSpawn, vrRoomSpawn;

	// Use this for initialization
	void Start () {
        meleeRoom.SetActive(false);
        vrRoom.SetActive(false);
        endRoom.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else if (Input.GetKeyDown(KeyCode.Alpha9)) {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            meleeRoom.SetActive(true);
            character.position = meleeSpawn.position;
        } else if (Input.GetKeyDown(KeyCode.Alpha0)) {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            meleeRoom.SetActive(true);
            vrRoom.SetActive(true);
            endRoom.SetActive(true);
            character.position = vrRoomSpawn.position;
        }
    }
}