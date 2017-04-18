using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloorColor : MonoBehaviour {
    GameObject[] Floors;
    GameObject player, Startp;
	public bool startTile;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("player");
        Startp = GameObject.FindGameObjectWithTag("StartP");
        Floors = GameObject.FindGameObjectsWithTag("Path");
        InvokeRepeating("CheckD", 1.0f, 1.0f);
        foreach (Transform t in transform) {
            t.gameObject.tag = "Path1";
            t.GetComponent<Collider>().isTrigger = true;
        }
    }

     void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "player" && gameObject.tag == "Path") {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            for (int i = 0; i < Floors.Length; i++) {
                float distance = Vector3.Distance(gameObject.transform.position, Floors[i].transform.position);
                if(distance <= 3.5) {
                    Floors[i].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }

				if(distance > 3.5 && !Floors[i].GetComponent<ChangeFloorColor>().startTile) {
                    Floors[i].gameObject.GetComponent<Renderer>().material.color = Color.blue;
                }
            }
        }

        if(other.gameObject.tag == "player" && gameObject.tag != "Path" && gameObject.tag != "Path1") {
            other.transform.position = Startp.transform.position;
        }
    }

    void CheckD() {
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (distance > 4 && !startTile) {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}