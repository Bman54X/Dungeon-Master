using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
    public class hpVR : MonoBehaviour {
        public int hp = 100;
        public Transform parent1;
        public bool hitonce = false;
        private MoveIfDead mid;
        public int deadTracker = 0;
        public GameObject rangeWall;
        public bool spawnedInRangeRoom = false;
        public bool spawnedInCroom = false;
        public GameObject bow;
        public GameObject arrow;
        public GameObject sword;
        public Hand hand1;
        public Hand hand2;
        // Use this for initialization
        void Start() {
            //  parent1 = gameObject.transform.root;
            mid = parent1.GetComponentInChildren<MoveIfDead>();
        }

        // Update is called once per frame
        void Update() {
            if (hp < 1) {
                sword = GameObject.FindGameObjectWithTag("vrSword");
                if (hand1.GetComponentInChildren<GetSpeed>() != null) {
                    hand1.DetachObject(sword.gameObject);
                }else {
                    hand2.DetachObject(sword.gameObject);
                }
                 //   mid.dead = true;
                if (spawnedInCroom == true)
                {
                 //   hand1.DetachObject()
                    mid.vrPlatR = true;
                }
                if (spawnedInRangeRoom == true)
                {
                    bow = GameObject.Find("Longbow(Clone)");
                    arrow = GameObject.Find("ArrowHand(Clone)");
                    hand1 = hand1.gameObject.GetComponentInParent<Hand>();    
                    hand2 = hand2.gameObject.GetComponentInParent<Hand>();

                    //   Instantiate(SwordS, c.gameObject.transform.position, c.gameObject.transform.rotation);

                    if (hand1.GetComponentInChildren<Longbow>() != null){
                        hand1.DetachObject(bow.gameObject);
                        hand2.DetachObject(arrow.gameObject);
                    }
                    else{
                       hand2.DetachObject(bow.gameObject);
                        hand1.DetachObject(arrow.gameObject);
                    }            

                    rangeWall.GetComponent<BoxCollider>().center = new Vector3(0, -4.5f, 0);
                    mid.cRoom = true;
                    spawnedInRangeRoom = false;
                    spawnedInCroom = true;
                }
                hp = 100;
            }
        }
        private void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.tag == "Sword" && c.gameObject.GetComponent<Sword>().swinging == true)
            {
                hp = hp - 20;
                Debug.Log("player sword hit vr");
            }
            if (c.gameObject.tag == "Arrow" && c.gameObject.GetComponent<ArrowProjectile>().shooter == "player")
            {
                Debug.Log(c.gameObject.name);
                hp = hp - 20;
            }
        }
        private void OnTriggerStay(Collider c)
        {
            if (c.gameObject.tag == "Sword" && c.gameObject.GetComponent<Sword>().swinging == true)
            {
                hp = hp - 20;
                Debug.Log("player sword hit vr");
                c.gameObject.GetComponent<Sword>().swinging = false;
            }
        }

    }
}