using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour {
    bool sTrue = true;
    bool sFalse = false;
    bool ct;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        gameObject.SetActive(true);
        ActiveTrue();
    }
    public void ActiveTrue()
    {
       gameObject.SetActive(true);
        ct = sTrue;
    }
    public void ActiveFalse()
    {
       gameObject.SetActive(false);
        ct = sFalse;
    }
}
