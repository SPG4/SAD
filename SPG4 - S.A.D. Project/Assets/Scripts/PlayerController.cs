using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int playerNumber;

    public float speed = 3f;
    public float maxSpeed = 10f;
    public float jumpForce = 5000f;

    public bool jumpState;
    public bool oldJumpState;
    public bool grounded;

    public Vector2 position;
    public Rigidbody2D ridgidbodyPlayer;

    private float h;

    private Vector2 velocity;

    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        ridgidbodyPlayer = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        position.x = transform.position.x;
        position.y = transform.position.y;
    }

    private void FixedUpdate()
    {
        Movement();
        SpeedLimit();

        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump" + playerNumber);

        if (jumpState)
        {
            Jump();
        }
    }

    private void Movement()
    {
        h = Input.GetAxis("Horizontal" + playerNumber);

        velocity.x = (speed * h);
        velocity.y = this.ridgidbodyPlayer.velocity.y;

        this.ridgidbodyPlayer.velocity = velocity;
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
