using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
    public class EnemyHitTest : MonoBehaviour
    {
        public GameObject cPlayer;
        public GameObject SwordS;
        public GameObject SwordR;
        private Vector3 BackWardsV;
        private Hand hand;
        private Character character;
        private GetSpeed Gspeed;
        // Use this for initialization
        void Start()
        {
            //  SwordR = GameObject.FindGameObjectWithTag("Sword");
            //   Gspeed = SwordR.gameObject.GetComponent<GetSpeed>();
            cPlayer = GameObject.FindGameObjectWithTag("player");
        }

        // Update is called once per frame
        void Update()
        {
           SwordR = GameObject.FindGameObjectWithTag("Sword");
            Gspeed = SwordR.gameObject.GetComponent<GetSpeed>();
         //   Debug.Log(Gspeed.speed);            
        }
        void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.tag == "Sword")
            {
             //   Debug.Log(SwordR);               
                Debug.Log("sword hit enemy");
               character = cPlayer.gameObject.GetComponent<Character>();
               character.health -= 10;
                hand = c.gameObject.GetComponentInParent<Hand>();                      
                c.gameObject.transform.position = c.gameObject.transform.position - c.gameObject.transform.right;
              Instantiate(SwordS, c.gameObject.transform.position, c.gameObject.transform.rotation);
                hand.DetachObject(c.gameObject);
            }
            if(c.gameObject.tag == "Arrow")
            {
                Debug.Log("arrow hit enemy");
                character = cPlayer.gameObject.GetComponent<Character>();
                character.health -= 10;
            }
            
        }
    }
}