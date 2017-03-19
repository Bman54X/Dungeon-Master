using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewRockCrossbow : MonoBehaviour
{
    public float rockRespawnTime = 3.0f;
    public bool rock = false;
    public GameObject rockPrefab;
    public Transform rockSpawnPoint;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()  {
        if (rock == true){
            rockRespawnTime = rockRespawnTime - Time.deltaTime;
            if (rockRespawnTime <= 0){
                //spawn new bomb
                GameObject clone = Instantiate(rockPrefab, rockSpawnPoint.position, Quaternion.identity);
                //  clone.transform.parent = vrThrowingplat.transform;
                rock = false;
                rockRespawnTime = 3.0f;
            }
        }
    }
}
