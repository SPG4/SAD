using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Teleport antigrav field")
        {
            AntiGravityEyeBall();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Teleport antigrav field")
        {
            ReturnToNormal();
        }
    }

    private void AntiGravityEyeBall()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;

        Vector2 CurrentVelocity = GetComponent<Rigidbody2D>().velocity;
        CurrentVelocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x * 0.9f, GetComponent<Rigidbody2D>().velocity.y * 0.9f);

        GetComponent<Rigidbody2D>().velocity = CurrentVelocity;
    }

    private void ReturnToNormal()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
