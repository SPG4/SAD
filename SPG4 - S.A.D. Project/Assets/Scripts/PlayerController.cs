using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 3f;
    public float maxSpeed = 10f;
    public float jumpForce = 5000f;

    public bool jumpState;
    public bool oldJumpState;

    public Vector2 velocity;
    public Rigidbody2D ridgidbodyPlayer;

    // Use this for initialization
    void Start ()
    {
        ridgidbodyPlayer = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    private void FixedUpdate()
    {
        Movement();
        SpeedLimit();

        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump");
        if (jumpState)
        {
            Jump();
        }
    }

    private void Movement()
    {
        float h = Input.GetAxis("Horizontal");

        velocity.x = (speed * h);
        velocity.y = ridgidbodyPlayer.velocity.y;

        ridgidbodyPlayer.velocity = velocity;
    }

    public void Jump()
    {
        if (jumpState && !oldJumpState)
        {
            ridgidbodyPlayer.velocity = new Vector2(ridgidbodyPlayer.velocity.x, 0);
            velocity = ridgidbodyPlayer.velocity;
            ridgidbodyPlayer.AddForce(Vector2.up * jumpForce);
        }
    }

    private void SpeedLimit()
    {
        if (ridgidbodyPlayer.velocity.x > maxSpeed)
        {
            ridgidbodyPlayer.velocity = new Vector2(maxSpeed, ridgidbodyPlayer.velocity.y);
        }

        else if (ridgidbodyPlayer.velocity.x < -maxSpeed)
        {
            ridgidbodyPlayer.velocity = new Vector2(-maxSpeed, ridgidbodyPlayer.velocity.y);
        }
    }
}
