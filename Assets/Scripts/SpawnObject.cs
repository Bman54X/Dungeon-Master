using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {
    public GameObject prefab;
    public Transform prefab1;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public void SpawnObjectH()
    {
        Instantiate(prefab, prefab1);
    }
}
