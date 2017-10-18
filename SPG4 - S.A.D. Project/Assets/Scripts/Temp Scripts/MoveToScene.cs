using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Level 1 - Prototype");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
