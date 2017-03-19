using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnRockCR : MonoBehaviour {
    public GameObject rockRespawn;
    private SpawnNewRockCrossbow pobjectM;
    public GameObject pObjectMan;
    // Use this for initialization
    void Start () {
        pobjectM = pObjectMan.GetComponent<SpawnNewRockCrossbow>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void setTrueRock()
    {
        pobjectM.rock = true;
    }
}
