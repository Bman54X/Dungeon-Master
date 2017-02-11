using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEArrowHit : MonoBehaviour {
    public Rigidbody Spike;
 
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Arrow")
        {
            for (int i = 0; i < 30; i++)
            {
                Rigidbody clone = Instantiate(Spike, transform.position, transform.rotation);
                int randomX = Random.Range(-360, 360);
                int randomY = Random.Range(-360, 360);
                int randomZ = Random.Range(-360, 360);
                
                Vector3 randomDir = new Vector3(randomX, randomY, randomZ);
                // clone.velocity = transform.TransformDirection(new Vector3(randomX,randomY, randomZ));
                clone.velocity = randomDir * 0.09f;
                //if(other.gameObject.tag == "player")
                Debug.Log("AoE Damage");
            }
        }
    }
}
