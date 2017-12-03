using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBlackHole : MonoBehaviour {

    GameObject particles;
    float endTimer = 0;
    
    // Use this for initialization
	void Start () {
        particles = gameObject.transform.Find("BlackHole_2_exit_particles").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.childCount == 1)
        {
            endTimer += Time.deltaTime;
            gameObject.transform.localScale *= 1.07f;
            particles.transform.localScale *= 1.07f;
        }

        if (endTimer > 1.0f)
            SceneManager.LoadScene("Level 5");
    }
}
