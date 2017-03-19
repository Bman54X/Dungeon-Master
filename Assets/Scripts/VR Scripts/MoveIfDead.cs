using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIfDead : MonoBehaviour {
    public bool dead = false;
    public bool vrPlatR = false;
    public bool crossbowVRpr = false;
    public Transform crossbowVRP;
    public bool cRoom = false;
    public bool rRoom = false;
    public GameObject skeletonP;
    public GameObject throwingPlat;
    public Transform deadP;
    public Transform roomP;
    public Transform VRPLAT;
    public Transform VRplat;
    public GameObject VRPLAT2;
    public Transform VRplat2;
    public Transform VRPLAT3;
    public Transform VRPLAT4;
    public Transform VRplat3;
    public Transform VRplat4;
    private MovePlayerIfHit movePifHit1;
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
        movePifHit1 = VRplat.GetComponent<MovePlayerIfHit>();
        movePifHit2 = VRPLAT2.GetComponent<MovePlayerIfHit>();
        movePifHit3 = VRPLAT3.GetComponent<MovePlayerIfHit>();
        movePifHit4 = VRPLAT4.GetComponent<MovePlayerIfHit>();
     //   skeletonP.transform.position = VRPLAT.position;
     //   transform.position = VRPLAT.position;
	}
	// Update is called once per frame
	void Update () {
        if(crossbowVRpr == true)
        {
            transform.position = crossbowVRP.position;
            skeletonP.transform.position = crossbowVRP.position;
            crossbowVRpr = false;
        }
		if (dead == true){
          //  transform.position = deadP.position;
        }
        if(dead == false){
          // transform.position = roomP.position;
         
        }
        if(movePifHit1.moveP1 == true){
            skeletonP.transform.position = VRPLAT.position - new Vector3(0, 0.0f, 2);
            transform.position = VRPLAT.position - new Vector3(0, 0.0f, 2);
            throwingPlat.transform.position = VRPLAT.position + new Vector3(0, 0.4f, 0);
            movePifHit1.moveP1 = false;
        }
        if(movePifHit2.moveP2 == true){
            skeletonP.transform.position = VRplat2.transform.position - new Vector3(0, 0.0f, 2);
            transform.position = VRplat2.transform.position -  new Vector3(0, 0.0f, 2);
            throwingPlat.transform.position = VRplat2.transform.position + new Vector3(0, 0.4f, 0);
            movePifHit2.moveP2 = false;
        }
        if (movePifHit3.moveP3 == true){
            skeletonP.transform.position = VRplat3.transform.position - new Vector3(0, 0.0f, 2); 
            transform.position = VRplat3.transform.position - new Vector3(0, 0.0f, 2); 
            throwingPlat.transform.position = VRplat3.transform.position + new Vector3(0, 0.4f, 0);
            movePifHit3.moveP3 = false;
        }
        if (movePifHit4.moveP4 == true){
            skeletonP.transform.position = VRplat4.transform.position - new Vector3(0, 0.0f, 2); 
            transform.position = VRplat4.transform.position - new Vector3(0, 0.0f, 2); 
            throwingPlat.transform.position = VRplat4.transform.position + new Vector3(0, 0.4f, 0);
            movePifHit4.moveP4 = false;
        }
        if(cRoom == true)
        {
            skeletonP.transform.position = combatRoomSpawnPoint.transform.position;
            transform.position = combatRoomSpawnPoint.transform.position;
            cRoom = false;
        }
        if (rRoom == true)
        {
            skeletonP.transform.position = rangeRoomSpawnPoint.transform.position;
            transform.position = rangeRoomSpawnPoint.transform.position;
            rRoom = false;
        }
    }
}
