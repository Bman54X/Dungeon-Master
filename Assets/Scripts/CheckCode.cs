using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCode : MonoBehaviour {
    public GameObject N1;
    public GameObject N2;
    public GameObject N3;
    // Use this for initialization
    void Start () {
        N1 = GameObject.Find("N1");
        N2 = GameObject.Find("N2");
        N3 = GameObject.Find("N3");
    }
	
	// Update is called once per frame
	void Update () {
        checkCode();
	}
    void checkCode()
    {
        if (N1 != null)
        {
            if (N1.GetComponentInChildren<TextMesh>().text == 1.ToString() && N2.GetComponentInChildren<TextMesh>().text == 2.ToString() && N3.GetComponentInChildren<TextMesh>().text == 3.ToString())
            {
                Destroy(N1);
            }
        }
    }
}
