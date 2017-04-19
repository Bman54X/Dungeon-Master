using UnityEngine;
namespace Valve.VR.InteractionSystem {
    public class MoveDoor : MonoBehaviour {
        public Transform door, doorStart, doorEnd;
        Transform target;
        float speed = 8.0f;
        public GameObject vrPlayer, hpREF;
        private MoveIfDead mid;
        private hpVR hpvr;
        bool alreadyChanged = false;
        public bool runThisOnce = true;

        // Use this for initialization
        void Start() {
            mid = vrPlayer.gameObject.GetComponent<MoveIfDead>();
            hpvr = hpREF.gameObject.GetComponent<hpVR>();
            target = doorStart;
        }

        // Update is called once per frame
        void Update() {
            if (door.position != target.position) {
                door.position = Vector3.MoveTowards(door.position, target.position, speed * Time.deltaTime);
            }
        }

        void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "player" && !alreadyChanged) {
                changeTarget();
                if (runThisOnce == true) {
                    mid.rRoom = true;
                    hpvr.spawnedInRangeRoom = true;
                    runThisOnce = false;
                }
            }
        }

        public void changeTarget() {
            target = doorEnd;
            alreadyChanged = true;
        }
    }
}