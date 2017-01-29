using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNumber : MonoBehaviour {
    int i = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void changeNumber()
    {
        GetComponent<TextMesh>().text = i.ToString();
        i++;
        if (i == 11)
        {
            i = 0;
            GetComponent<TextMesh>().text = i.ToString();
        }
    }
     void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player" && Input.GetKeyDown("space"))
        {
            changeNumber();
        }
    }
}
