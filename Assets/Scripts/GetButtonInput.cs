using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem {
    public class GetButtonInput : MonoBehaviour {
        private Hand hand;
        // Use this for initialization
        void Start() {
            hand = gameObject.GetComponent<Hand>();
        }

        // Update is called once per frame
        void Update() {
          //  hand.GetStandardInteractionButtonDown();
         //   Debug.Log(hand.buttontester);
        }
    }
}