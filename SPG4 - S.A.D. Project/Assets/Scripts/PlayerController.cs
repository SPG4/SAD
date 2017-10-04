using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int playerNumber;

    public float speed = 3f;
    public float maxSpeed = 10f;
    public float crouchSpeed = 2f;
    public float jumpForce = 5000f;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public Vector2 position;
    public Rigidbody2D ridgidbodyPlayer;

    private bool jumpState;
    private bool oldJumpState;
    private bool grounded;
    private bool hasDoubleJumped;
    private bool isOnWall;
    private bool crouchState;

    private float horizontalInput;
    private Vector2 velocity;

    private Animator animator;

    private GameObject defaultCollider;
    private GameObject crouchCollider;

    public AudioSource jumping;


    // Use this for initialization
    void Start ()
    {
        ridgidbodyPlayer = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        defaultCollider = transform.Find("Default Collider").gameObject;
        crouchCollider = transform.Find("Crouch Collider").gameObject;

        crouchCollider.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (grounded)
            hasDoubleJumped = false;

        position.x = transform.position.x;
        position.y = transform.position.y;

        if (crouchState)
        {
            defaultCollider.SetActive(false);
            crouchCollider.SetActive(true);
        }
        if (!crouchState)
        {
            defaultCollider.SetActive(true);
            crouchCollider.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isOnWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump" + playerNumber);
        crouchState = Input.GetButton("Crouch" + playerNumber);

        Movement();
        SpeedLimit();
        TurnToInputDirection();

        if (jumpState && !oldJumpState && grounded)
        {
            Jump();
            jumping.Play();

        }

        if (jumpState && !oldJumpState && !grounded && !hasDoubleJumped)
        {
            Jump();
            hasDoubleJumped = true;
            jumping.Play();

        }

        if (isOnWall)
        {
            grounded = false;
            hasDoubleJumped = false;
        }
    }

    private void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal" + playerNumber);

        if (crouchState)
            velocity.x = (crouchSpeed * horizontalInput);

        else
            velocity.x = (speed * horizontalInput);

        velocity.y = this.ridgidbodyPlayer.velocity.y;

        this.ridgidbodyPlayer.velocity = velocity;
    }

    public void Jump()
    {
        ridgidbodyPlayer.velocity = new Vector2(ridgidbodyPlayer.velocity.x, 0);
        velocity = ridgidbodyPlayer.velocity;
        ridgidbodyPlayer.AddForce(Vector2.up * jumpForce);
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

    private void TurnToInputDirection()
    {
        if (Input.GetAxis("Horizontal" + playerNumber) < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal" + playerNumber) > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
