using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playthiseffectd : MonoBehaviour {
    public ParticleSystem self;
    public bool playit;
	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
        if (playit) {
        //    play();
        } else {
         //   stop();
        }
	}
    public void play(){
         self.Play();
    }
    public void stop() {
        self.Stop();
    }

}
