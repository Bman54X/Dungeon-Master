using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
    public class EnemyHitTest : MonoBehaviour
    {
        public GameObject SwordS;
        public GameObject SwordR;
        private Vector3 BackWardsV;
        private Hand hand;
        private GetSpeed Gspeed;
        // Use this for initialization
        void Start()
        {
          //  SwordR = GameObject.FindGameObjectWithTag("Sword");
         //   Gspeed = SwordR.gameObject.GetComponent<GetSpeed>();
        }

        // Update is called once per frame
        void Update()
        {
            SwordR = GameObject.FindGameObjectWithTag("Sword");
            Gspeed = SwordR.gameObject.GetComponent<GetSpeed>();
            Debug.Log(Gspeed.speed);            
        }
        void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.tag == "Sword" && Gspeed.speed > 2)
            {
                Debug.Log(SwordR);               
                Debug.Log("hit enemy");
                hand = c.gameObject.GetComponentInParent<Hand>();                      
                c.gameObject.transform.position = c.gameObject.transform.position - c.gameObject.transform.right;
                Instantiate(SwordS, c.gameObject.transform.position, c.gameObject.transform.rotation);
                hand.DetachObject(c.gameObject);


            }
            
        }
    }
}