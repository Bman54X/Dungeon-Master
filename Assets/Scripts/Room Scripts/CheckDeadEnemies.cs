using UnityEngine;
namespace Valve.VR.InteractionSystem {
    public class CheckDeadEnemies : MonoBehaviour {
        public GameObject[] enemies;
        PlatformMover door;
        bool activated;
        public GameObject vrPlayer, hpREF;
        public GameObject vrRoom, endRoom;
        private MoveIfDead mid;
        private hpVR hpvr;

        // Use this for initialization
        void Start() {
            activated = false;
            door = gameObject.GetComponent<PlatformMover>();
            door.setActivation(false);
            mid = vrPlayer.gameObject.GetComponent<MoveIfDead>();
            hpvr = hpREF.gameObject.GetComponent<hpVR>();

        }

        // Update is called once per frame
        void Update() {
            if (!activated) {
                foreach (GameObject enemy in enemies) {
                    if (enemy != null && hpvr.diedinCroom != true) {
                        activated = false;
                        break;
                    } else {
                        activated = true;
                    }
                }

                if (activated) {
                    mid.vrPlatR = true;
                    door.setActivation(true);
                    vrRoom.SetActive(true); endRoom.SetActive(true);
                }
            }
        }
    }
}