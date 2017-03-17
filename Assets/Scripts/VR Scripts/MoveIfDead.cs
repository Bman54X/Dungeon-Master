using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIfDead : MonoBehaviour {
    public bool dead = false;
    public bool cRoom = false;
    public bool rRoom = false;
    public GameObject skeletonP;
    public GameObject throwingPlat;
    public Transform deadP;
    public Transform roomP;
    public Transform VRPLAT;
    public GameObject VRPLAT2;
    public Transform VRplat2;
    public Transform VRPLAT3;
    public Transform VRPLAT4;
    public Transform VRplat3;
    public Transform VRplat4;
    private MovePlayerIfHit movePifHit2;
    private MovePlayerIfHit movePifHit3;
    private MovePlayerIfHit movePifHit4;
    public GameObject pOBJref;
    private pObjectManager pOBJ;
    public GameObject vrBomb;
    public Transform combatRoomSpawnPoint;
    public Transform rangeRoomSpawnPoint;
    // Use this for initialization
    void Start () {

        movePifHit2 = VRPLAT2.GetComponent<MovePlayerIfHit>();
        movePifHit3 = VRPLAT3.GetComponent<MovePlayerIfHit>();
        movePifHit4 = VRPLAT4.GetComponent<MovePlayerIfHit>();
        skeletonP.transform.position = VRPLAT.position;
        transform.position = VRPLAT.position;
	}
	// Update is called once per frame
	void Update () {
		if (dead == true){
          //  transform.position = deadP.position;
        }
        if(dead == false){
          // transform.position = roomP.position;
         
        }
        if(movePifHit2.moveP2 == true){
            skeletonP.transform.position = VRplat2.transform.position;
            transform.position = VRplat2.transform.position;
            throwingPlat.transform.position = VRplat2.transform.position + new Vector3(2, 0, 0);
            movePifHit2.moveP2 = false;
        }
        if (movePifHit3.moveP3 == true){
            skeletonP.transform.position = VRplat3.transform.position;
            transform.position = VRplat3.transform.position;
            throwingPlat.transform.position = VRplat3.transform.position + new Vector3(2, 0, 0);
            movePifHit3.moveP3 = false;
        }
        if (movePifHit4.moveP4 == true){
            skeletonP.transform.position = VRplat4.transform.position;
            transform.position = VRplat4.transform.position;
            throwingPlat.transform.position = VRplat4.transform.position + new Vector3(2, 0, 0);
            movePifHit4.moveP4 = false;
        }
        if(cRoom == true)
        {
            skeletonP.transform.position = combatRoomSpawnPoint.transform.position;
            transform.position = combatRoomSpawnPoint.transform.position;
        }
        if (rRoom == true)
        {
            skeletonP.transform.position = rangeRoomSpawnPoint.transform.position;
            transform.position = rangeRoomSpawnPoint.transform.position;
        }
    }
}
