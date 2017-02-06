using UnityEngine;
using System.Collections;

public class GetSpeed : MonoBehaviour {
    public float speed;
    Vector3 previous;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        speed = (transform.position -previous).magnitude / Time.deltaTime;
        previous = transform.position;
     //   Debug.Log(speed);
	}
}
