using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour {
    string _shooter;

    // Use this for initialization
    void Start() {
        //Destroy(gameObject, 4.0f);
    }

    public string shooter
    {
        get { return _shooter; }
        set { _shooter = value; }
    }
}