using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePlayersTo : MonoBehaviour {
    public GameObject teleToThis;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.transform.position = teleToThis.transform.position;
        }
    }

}
