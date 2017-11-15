using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes so players and other objects (doesnt work on on tag: Ground)
/// can stand still on moving platforms.
/// </summary>
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
        if (collision.gameObject.tag != "Ground")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            collision.rigidbody.transform.SetParent(null);
        }
    }
}
