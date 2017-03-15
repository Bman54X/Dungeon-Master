using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUpOnHit2 : MonoBehaviour {
    private BlowUpOnHit blowupH;
    public float timer = 5.0f;
	// Use this for initialization
	void Start () {
        blowupH = gameObject.GetComponentInParent<BlowUpOnHit>();
	}
	
	// Update is called once per frame
	void Update () {
        if (blowupH.objectThrown == true)  {
            timer = timer - Time.deltaTime;
         //   Debug.Log(timer);
           }
        if (blowupH.objectThrown == true){
            if (timer < 0){
                Explosion();
                Debug.Log("destorying object");
                Destroy(transform.parent.gameObject);
            }
        }
    }
  
  
    private void Explosion(){

    }
}
