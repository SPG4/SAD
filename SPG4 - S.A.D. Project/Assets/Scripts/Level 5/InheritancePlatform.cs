using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritancePlatform : MonoBehaviour {
    [Tooltip("Move with target object")]
    public GameObject moveWith;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = moveWith.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        //{
            collision.collider.transform.SetParent(transform);
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        //{
            collision.rigidbody.transform.SetParent(null);
        //}
    }
}
