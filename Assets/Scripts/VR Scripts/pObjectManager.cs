using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pObjectManager : MonoBehaviour {
    public GameObject vrThrowingplat;
    public GameObject bombPrefab;
    public GameObject rockPrefab;
    public Transform bombSpawnPoint;
    public Transform rockSpawnPoint;
    public bool bomb;
    public bool arrow;
    public bool rock = true;
    public int rockAmount = 0;
    public float bombRespawnTime = 10.0f;
    public float arrowRespawnTime = 10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(bomb == true){
            bombRespawnTime = bombRespawnTime - Time.deltaTime;
            if (bombRespawnTime <= 0){
                //spawn new bomb
              GameObject clone =  Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity);
                clone.transform.parent = vrThrowingplat.transform;
                bomb = false;
                bombRespawnTime = 10.0f;
            }
          //  Debug.Log("bomb resapwn");
        }
        if (arrow == true){
            arrowRespawnTime = arrowRespawnTime - Time.deltaTime;
            if (arrowRespawnTime <= 0){
                //spawn new arrow
                bomb = false;
            }
            Debug.Log("bomb resapwn");
        }
        if(rock == true){
            if (rockAmount < 5){
                spawnNewRock();      
            }
        }
    }
   public  void spawnNewRock(){
        //spawn rock
        GameObject clone = Instantiate(rockPrefab, rockSpawnPoint.position, Quaternion.identity);
        clone.transform.parent = vrThrowingplat.transform;
       
    }
}

