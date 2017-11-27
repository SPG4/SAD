using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballController : MonoBehaviour {

    Rigidbody2D eyeballRidgidbody;
    BoxCollider2D respawnTrigger;
    Vector2 startPos;

	// Use this for initialization
	void Start () {
        startPos = gameObject.transform.position;
        respawnTrigger = GameObject.Find("Respawn trigger").GetComponent<BoxCollider2D>();
        eyeballRidgidbody = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Eyeball respawn trigger")
        {
            gameObject.transform.position = startPos;
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            eyeballRidgidbody.velocity = Vector2.zero;
        }
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
        gameObject.layer = LayerMask.NameToLayer("Interactable ball object");
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
