using UnityEngine;

namespace Valve.VR.InteractionSystem {
    public class Door : MonoBehaviour {
        public Transform start, end;
        int numActivations = 2, totalActivations;
        float speed;
        bool activated;
        public GameObject vrPlayer, hpREF;
        private MoveIfDead mid;
        private hpVR hpvr;
        public bool runThisOnce = true;
        public GameObject meleeRoom;

        void Awake() {
            mid = vrPlayer.gameObject.GetComponent<MoveIfDead>();
            hpvr = hpREF.gameObject.GetComponent<hpVR>();
            activated = true;
            speed = 5.0f;
        }

        void FixedUpdate() {
            if (activated) {
                transform.position = Vector3.MoveTowards(transform.position, end.position, speed * Time.deltaTime);
                if (runThisOnce == true) {
                    mid.cRoom = true;
                    hpvr.spawnedInCroom = true;
                    runThisOnce = false;
                    meleeRoom.SetActive(true);
                }
            } else {
                transform.position = Vector3.MoveTowards(transform.position, start.position, speed * Time.deltaTime);
            }
        }

        public void setActivation(bool active) {
            if (active) {
                totalActivations++;
                if (totalActivations == numActivations) {
                    activated = active;
                }
            } else {
                if (totalActivations != 0) {
                    totalActivations--;
                }
                activated = active;
            }
        }
    }
}