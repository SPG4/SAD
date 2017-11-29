using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBlackHole : MonoBehaviour {

    GameObject particles;
    
    // Use this for initialization
	void Start () {
        particles = gameObject.transform.Find("BlackHole_2_exit_particles").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.childCount == 1)
        {
            gameObject.transform.localScale *= 1.07f;
            particles.transform.localScale *= 1.07f;
        }
	}
}
