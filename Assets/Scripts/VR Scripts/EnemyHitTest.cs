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
            //cPlayer = GameObject.FindGameObjectWithTag("player");
			character = GameObject.FindGameObjectWithTag("player").GetComponent<Character>();
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
            if(c.gameObject.tag == "vrRock")
            {
                character.takeDamage(5);
            }
            if(c.gameObject.tag =="vrBomb")
            {
                character.takeDamage(10);
                Debug.Log("vrbomb");
            }
            if (c.gameObject.tag == "vrSword")
            {
             //   Debug.Log(SwordR);               
                Debug.Log("sword hit enemy");
               
				character.takeDamage(20);
                hand = c.gameObject.GetComponentInParent<Hand>();                      
                c.gameObject.transform.position = c.gameObject.transform.position - c.gameObject.transform.right;
                Instantiate(SwordS, c.gameObject.transform.position, c.gameObject.transform.rotation);
                hand.DetachObject(c.gameObject);
            } else if(c.gameObject.tag == "Arrow" && c.gameObject.GetComponent<ArrowProjectile>().shooter == "enemy")
            {
                Destroy(c.gameObject);
				character.takeDamage(10);
            }
        }
    }
}