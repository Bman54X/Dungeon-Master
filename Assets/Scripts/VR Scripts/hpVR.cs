using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpVR : MonoBehaviour {
    public int hp = 100;
    public Transform parent1;
    
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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        hp = hp - 95;
    }

}
