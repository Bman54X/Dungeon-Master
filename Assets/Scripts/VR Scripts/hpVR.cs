using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpVR : MonoBehaviour {
    public int hp = 100;
    public Transform parent1;
    public bool hitonce = false;
    private MoveIfDead mid;
	// Use this for initialization
	void Start () {
        parent1 = gameObject.transform.root;
        mid = parent1.GetComponentInChildren<MoveIfDead>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hp < 10)
        {
            mid.dead = true;
        }	
	}
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Sword" && c.gameObject.GetComponent<Sword>().swinging == true)
        {
            
            Debug.Log("player sword hit vr");
        }
        if (c.gameObject.tag == "Arrow" && c.gameObject.GetComponent<ArrowProjectile>().shooter == "player")
        {
            Debug.Log(c.gameObject.name);
            hp = hp - 95;
        }
    }
    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Sword" && c.gameObject.GetComponent<Sword>().swinging == true)
        {

            Debug.Log("player sword hit vr");
            c.gameObject.GetComponent<Sword>().swinging = false;
        }
    }

}
