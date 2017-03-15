using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pObjectManager : MonoBehaviour {
    public bool bomb;
    public bool arrow;
    public bool rock;
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
                bomb = false;
            }
            Debug.Log("bomb resapwn");
        }
        if (arrow == true){
            arrowRespawnTime = arrowRespawnTime - Time.deltaTime;
            if (arrowRespawnTime <= 0){
                //spawn new arrow
                bomb = false;
            }
            Debug.Log("bomb resapwn");
        }
        if(rock == true)
        {
            spawnNewRock();
            rock = false;
        }
    }
    void spawnNewRock()
    {
        //spawn rock
        rockAmount = rockAmount + 1;
    }
}

