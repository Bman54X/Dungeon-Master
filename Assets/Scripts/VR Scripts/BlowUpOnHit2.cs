using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUpOnHit2 : MonoBehaviour {
    private BlowUpOnHit blowupH;
    public GameObject explosionPrefab;
    public float timer = 5.0f;
	// Use this for initialization
	void Start () {
        blowupH = gameObject.GetComponentInParent<BlowUpOnHit>();
       // InvokeRepeating("Explosion", 2.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
       // GameObject clone = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);

        if (blowupH.objectThrown == true)  {
            timer = timer - Time.deltaTime;
         //   Debug.Log(timer);
           }
        if (blowupH.objectThrown == true){
            if (timer < 0){
                Explosion();
                Debug.Log("destorying object");
                Destroy(transform.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider c) {
        if (c.transform.name == "Platform") {
            GameObject emptyObject = new GameObject();
            emptyObject.transform.parent = c.transform;
            transform.parent = emptyObject.transform;
        }
    }

    private void Explosion(){
        GameObject clone = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(clone, 1.0f);
      //  clone.transform.parent = gameObject.transform;
    }
}
