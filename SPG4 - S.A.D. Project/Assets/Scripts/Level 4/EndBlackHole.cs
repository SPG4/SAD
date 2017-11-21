using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBlackHole : MonoBehaviour {
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.childCount == 0)
            gameObject.transform.localScale *= 1.07f;
	}
}
