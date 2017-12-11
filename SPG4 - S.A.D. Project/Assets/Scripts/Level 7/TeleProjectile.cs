using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleProjectile : MonoBehaviour {

    public GameObject teleToThis;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ProjectileTAM")
        {
            Debug.Log("HIT");
            collision.transform.position = teleToThis.transform.position;
        }
    }
}
