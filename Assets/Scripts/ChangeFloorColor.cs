using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloorColor : MonoBehaviour {
    public GameObject[] Floors;
    public GameObject player;
    public GameObject Startp;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("player");
        Startp = GameObject.FindGameObjectWithTag("StartP");
        //  gameObject.GetComponent<Renderer>().material.color = Color.red;
        Floors = GameObject.FindGameObjectsWithTag("Path");
        InvokeRepeating("CheckD", 1.0f, 1.0f);
        foreach (Transform t in transform)
        {
            t.gameObject.tag = "Path1";
        }
    //    gameObject.tag = "theTag";
    }
	
	// Update is called once per frame
	void Update () {
      
    }
     void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player" && gameObject.tag == "Path")
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            for (int i = 0; i < Floors.Length; i++)
            {
                float distance = Vector3.Distance(gameObject.transform.position, Floors[i].transform.position);
                if(distance < 1.5)
                {
                    Floors[i].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
                if(distance > 1.6)
                {
                    Floors[i].gameObject.GetComponent<Renderer>().material.color = Color.blue;
                }
            //    Debug.Log(distance);
            }
        }
        if(other.gameObject.tag=="player" && gameObject.tag != "Path" && gameObject.tag != "Path1")
        {
            other.transform.position = Startp.transform.position;
        }
    }
     void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {          
           // gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
    void CheckD()
    {
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (distance > 3)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
