using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour {
public GameObject resetObject;
    // Use this for initialization
    private Vector3 pos;

    private Rigidbody2D rb;
    void Start () {
        pos = resetObject.transform.position;
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == resetObject)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            collision.gameObject.transform.position = pos;
        }
    }
}
