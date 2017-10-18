using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Public fields
    public int playerNumber;

    public float speed = 3f;
    public float maxSpeed = 10f;
    public float crouchSpeed = 2f;
    public float jumpForce = 5000f;
    public bool shooting;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsObject;

    public Vector2 position;
    public Rigidbody2D ridgidbodyPlayer;
    public AudioSource jumping; 

    public GameObject teleportBall;
    public GameObject crosshair;

    public PlayerController otherPlayer;

    public Animator bodyAnimator;
    public Animator handsAnimator;

    //Private fields 
    private bool jumpState;
    private bool oldJumpState;
    private bool grounded;
    private bool hasDoubleJumped;
    private bool isOnWall;
    private bool crouchState;
    private bool standingOnObject;

    private float horizontalInput;
    private Vector2 velocity;

    //private Animator animator;

    private GameObject defaultCollider;
    private GameObject crouchCollider;


    /// <summary>
    /// initialize conponents of the player here
    /// </summary>
    void Start ()
    {
        //crosshair = GameObject.FindGameObjectWithTag("Crosshair");

        ridgidbodyPlayer = gameObject.GetComponent<Rigidbody2D>();
        //animator = gameObject.GetComponent<Animator>();

        defaultCollider = transform.Find("Default Collider").gameObject;
        crouchCollider = transform.Find("Crouch Collider").gameObject;

        crouchCollider.SetActive(false);
        //crosshair.SetActive(false);
    }
	
	/// <summary>
    /// Update is called once a frame
    /// </summary>
	void Update ()
    {
        if (Input.GetButtonDown("Fire" + playerNumber) && shooting == false)
        {
            ShootTeleportBall();
        }
        //else
        //    shooting = false;


        //Behöver kolla vilket håll spelaren står på och ändra riktningen som siktet roteras på efter det
        if (Input.GetAxis("Aim"+playerNumber) == 1)
        {
            //crosshair.SetActive(true);
            crosshair.SendMessage("RotateObject", -2);

        }

        if (Input.GetAxis("Aim" + playerNumber) == -1)
        {
            //crosshair.SetActive(true);
            crosshair.SendMessage("RotateObject", 2); 
        }

        //if (Input.GetKeyUp(KeyCode.N))
        //{
        //    crosshair.SetActive(false);
        //}

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    crosshair.SetActive(false);
        //}

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

    /// <summary>
    /// Update function for actions involving physics
    /// </summary>
    private void FixedUpdate()
    {
        /*
          Set player bools grounded and isOnWall based on if the Ground Check and Wall Check transforms
          (which are placed on the player) are overlaping the LayerMask that specifies what counts as a
          wall or ground.
        */ 
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isOnWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        standingOnObject = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsObject);

        bodyAnimator.SetBool("Ground", grounded);
        handsAnimator.SetBool("Ground", grounded);

        bodyAnimator.SetFloat("xSpeed", Mathf.Abs(horizontalInput));
        handsAnimator.SetFloat("xSpeed", Mathf.Abs(horizontalInput));

        //Jumpstate is used to check if the player has pressed the jump button
        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump" + playerNumber);
        crouchState = Input.GetButton("Crouch" + playerNumber);

        Movement();
        SpeedLimit();
        TurnToInputDirection();

        //Check if a player has pressed the jump button and is standing on ground
        if (jumpState && !oldJumpState && grounded)
        {
            Jump();           
        }

        //Check if a player is pressing jump in the air after the player has jumped once
        if (jumpState && !oldJumpState && !grounded && !hasDoubleJumped)
        {
            Jump();
            hasDoubleJumped = true;
        }

        //Check if a player is up against a wall, if so it counts as staning on the ground
        if (isOnWall || standingOnObject)
        {
            grounded = false;
            hasDoubleJumped = false;
        }
    }

    /// <summary>
    /// Gets the input value from the axis with the name Horizontal plus the players number. 
    /// The value is then multiplied with specified speed values that correspond to different
    /// states that the player is in, for example crouching and standing normally
    /// </summary>
    private void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal" + playerNumber);

        //If crouching the players speed in the X-axis is decreased
        if (crouchState)
            velocity.x = (crouchSpeed * horizontalInput);

        //Normal speed in the X-axis when not crouching
        else
            velocity.x = (speed * horizontalInput);

        //Velocity in the Y-axis is not affected by horizontal movement
        velocity.y = this.ridgidbodyPlayer.velocity.y;

        //Give the players ridgidbody the newly calculated velocity
        this.ridgidbodyPlayer.velocity = velocity;
    }

    /// <summary>
    /// Plays jumping sound and adds an upward directed force to the playes ridgidbody
    /// </summary>
    public void Jump()
    {
        jumping.Play();
        ridgidbodyPlayer.velocity = new Vector2(ridgidbodyPlayer.velocity.x, 0);
        velocity = ridgidbodyPlayer.velocity;
        ridgidbodyPlayer.AddForce(Vector2.up * jumpForce);
    }

    /// <summary>
    /// Checks if the velocity of the players ridgidbody exceeds the maxSpeed value in both
    /// the positive and negative X-axis, if so the velocity is set to the positive or negative
    /// maxSpeed value respectively
    /// </summary>
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

    /// <summary>
    /// Checks if the input value on the X-axis is positive or negative and changes the players
    /// facing direction accordingly
    /// </summary>
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

    /// <summary>
    /// Sets a new position after a player has been teleported
    /// </summary>
    /// <param name="position"></param>
    void Teleport(Vector3 position)
    {
        gameObject.transform.position = position;
    }

    /// <summary>
    /// Creates an instance of the prefab teleportBall at the players position and gives the ball 
    /// the tags for the player that shot the ball and the player that will be teleported by it 
    /// </summary>
    void ShootTeleportBall()
    {
        Vector2 shootingDirection = crosshair.transform.position - wallCheck.transform.position;
        shootingDirection.Normalize();

        Debug.Log(shootingDirection);

        float speed = 15;
        
        shooting = true;

        GameObject ball = Instantiate(teleportBall, wallCheck.position, wallCheck.rotation);
        ball.GetComponent<ShootBall>().playerShootingString = gameObject.tag;
        ball.GetComponent<ShootBall>().playerBeingTeleportedString = otherPlayer.tag;
        ball.SendMessage("AddSpeedToBall", shootingDirection * speed);
    }
}
