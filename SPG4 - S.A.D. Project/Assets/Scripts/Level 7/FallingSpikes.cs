using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikes : MonoBehaviour {
    public float spikeTime;
    public bool falling;
    public bool fallTime;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (fallTime == true)
        {
            spikeTime += Time.deltaTime;
        }
        if (spikeTime >= 10.0f)
        {
            falling = true;
        }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            
            fallTime = true;
        }
    }
}
