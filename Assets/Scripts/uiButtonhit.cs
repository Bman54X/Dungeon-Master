using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiButtonhit : MonoBehaviour {
    private Betterc bc;

    public GameObject S1;
    public bool b1 = false;
    public bool b1a = false;

    public GameObject S3;
    public bool b3 = false;
    public bool b3a = false;
    // Use this for initialization
    void Start()
    {
        bc = GameObject.FindGameObjectWithTag("BC").GetComponent<Betterc>();
        S1 = GameObject.FindGameObjectWithTag("S1");
     ///   S1.SetActive(false);        
    }
	// Update is called once per frame
	void Update () {
        
      
  


    }
    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "R1" && bc.TriggerP == true)
        {      
            if (b1a == true)
            {
                S1.SetActive(false);
                b1a = false;
            }
            if (b1 == false)
            {
                S1.SetActive(true);
                b1 = true;
                b1a = true;
            }
            if(b1a == false)
            {
                b1 = false;
            }

            bc.TriggerP = false;
            Debug.Log("Room1 button");
        }
        if (gameObject.tag == "R3" && bc.TriggerP == true)
        {
            if (b3a == true)
            {
                S3.SetActive(false);
                b3a = false;
            }
            if (b3 == false)
            {
                S3.SetActive(true);
                b3 = true;
                b3a = true;
            }
            if (b3a == false)
            {
                b3 = false;
            }

            bc.TriggerP = false;
            Debug.Log("Room1 button");
        }
        if (gameObject.tag == "E1")
        {
            Debug.Log("spawn enemy");
        }
      //  Debug.Log("Room1 button");
    }
}
