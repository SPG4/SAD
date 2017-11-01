using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    //Public fields
    public int playerNumber;

    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float maxSpeed = 10f;
    [SerializeField]
    private float crouchSpeed = 2f;
    [SerializeField]
    private float jumpForce = 5000f;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

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
    private bool facingRight;
    private bool shooting;

    private float horizontalInput;
    private float aimingSpeed;
    private float worldHalfSize;
    private Vector2 velocity;
    private Vector3 initialVector = Vector3.forward;
    private Vector3 worldSize;

    private Rigidbody2D ridgidbodyPlayer;
    private GameObject defaultCollider;
    private GameObject crouchCollider;

    private AudioSource jumping;

    public float jumpForce1 = 20f;
    public float jumpPushForce = 40f;

    bool wallJumped = false;
    bool wallJumping = false;

    /// <summary>
    /// initialize conponents of the player here
    /// </summary>
    void Start ()
    {
        ridgidbodyPlayer = gameObject.GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(1, 1, 1);
        facingRight = true;

        defaultCollider = transform.Find("Default Collider").gameObject;
        crouchCollider = transform.Find("Crouch Collider").gameObject;
        crouchCollider.SetActive(false);

        aimingSpeed = 70f;

        worldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        worldHalfSize = this.GetComponentInChildren<Renderer>().bounds.size.x / 2;

        jumping = gameObject.GetComponent<AudioSource>();   
    }
	
	/// <summary>
    /// Update is called once a frame
    /// </summary>
	void Update ()
    {
        float aimInput = Input.GetAxis("Aim" + playerNumber);

        AimCrosshairRelativeToPlayer(aimInput);

        //Jumpstate is used to check if the player has pressed the jump button
        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump" + playerNumber);

        //if (jumpState && !oldJumpState)
        //{
        //    if (grounded)
        //        Jump();

        //    else if (!hasDoubleJumped)
        //    {
        //        Jump();
        //        hasDoubleJumped = true;
        //    }

        //    else if (isOnWall)
        //        wallJumped = true;
        //}

        crouchState = Input.GetButton("Crouch" + playerNumber);

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

        bodyAnimator.SetBool("Ground", grounded);
        handsAnimator.SetBool("Ground", grounded);

        bodyAnimator.SetFloat("xSpeed", Mathf.Abs(horizontalInput));
        handsAnimator.SetFloat("xSpeed", Mathf.Abs(horizontalInput));

        Movement();
        SpeedLimit();

        if (!wallJumping)
            TurnToInputDirection();

        if (wallJumped)
        {
            ridgidbodyPlayer.velocity = new Vector2(jumpPushForce * (facingRight ? -1 : 1), jumpForce1);
            wallJumping = true;
            Flip();
            wallJumped = false;
        }

        if (ridgidbodyPlayer.velocity.y < 0)
        {
            wallJumping = false;
        }

        if (grounded)
        {
            hasDoubleJumped = false;
            wallJumping = false;
        }

        //Check if a player has pressed the jump button and is standing on ground
        if (jumpState && !oldJumpState && grounded)
        {
            Jump();
        }

        //Used to cancel a players jump if they release the jump button before reaching the pivot point
        if (Input.GetButtonUp("Jump" + playerNumber))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * .5f;
                ridgidbodyPlayer.velocity = new Vector2(ridgidbodyPlayer.velocity.x, velocity.y);
            }
        }

        ////Check if a player is pressing jump in the air after the player has jumped once
        if (jumpState && !oldJumpState && !grounded && !hasDoubleJumped)
        {
            Jump();
            hasDoubleJumped = true;
        }

        //Check if a player is up against a wall, if so it counts as staning on the ground
        if (jumpState && !oldJumpState && isOnWall && !grounded) //|| standingOnObject)
        {
            wallJumped = true;
            grounded = false;
            hasDoubleJumped = true;
        }

        //if (ridgidbodyPlayer.velocity.x > 0 && !facingRight)
        //{
        //    Flip();
        //}
        //else if (ridgidbodyPlayer.velocity.x < 0 && facingRight)
        //{
        //    Flip();
        //}
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

        if (grounded)
            this.ridgidbodyPlayer.velocity = velocity;

        else if ((horizontalInput != 0 && !wallJumping))
            this.ridgidbodyPlayer.velocity = velocity;

        worldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        Vector3 worldLeft = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
        Debug.Log("world " + (worldSize.x - worldHalfSize));
        Debug.Log("check this " + worldLeft);
        Debug.Log("player " + playerNumber + " " + ridgidbodyPlayer.position.x);

        if (ridgidbodyPlayer.position.x >= worldSize.x - worldHalfSize)
        {
            ridgidbodyPlayer.position = new Vector2(worldSize.x - worldHalfSize, ridgidbodyPlayer.position.y);
            Debug.Log("out of bounds");
        }
        else if (ridgidbodyPlayer.position.x <= (worldLeft.x + worldHalfSize))
        {
            ridgidbodyPlayer.position = new Vector2(worldLeft.x + worldHalfSize, ridgidbodyPlayer.position.y);
        }
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
            facingRight = false;
        }

        if (Input.GetAxis("Horizontal" + playerNumber) > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
    }

    void Flip()
    {
        //if (facingRight)
        //    facingRight = false;
        //else if (!facingRight)
        //    facingRight = true;

        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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

    public void ResetShootingValue(bool shootingValue)
    {
        shooting = shootingValue;
    }

    /// <summary>
    /// Checks if the player is facing left or right and if the player has given input to move crosshair,
    /// if player is facing left the crosshair gets a message which sets the initial direction of the crosshair
    /// to vector3.left, the direction is used to calculate the proper angle between the initial direction of the
    /// crosshair and the updated direction that is updated by sending a rotate value to the RotateCrosshair method.
    /// the direction of the rotation is determined by the value of the aimInput.
    /// </summary>
    /// <param name="aimInput"></param>
    void AimCrosshairRelativeToPlayer(float aimInput)
    {
        float rotateDegrees = 0f;

        if (facingRight && aimInput != 0)
        {
            crosshair.SendMessage("SetInitialVector", Vector3.right);

            if (aimInput == 1)
            {
                rotateDegrees -= aimingSpeed * Time.deltaTime;
            }

            else if (aimInput == -1)
            {
                rotateDegrees += aimingSpeed * Time.deltaTime;
            }

            crosshair.SendMessage("RotateCrosshair", rotateDegrees);
        }

        if (!facingRight && aimInput != 0)
        {
            crosshair.SendMessage("SetInitialVector", Vector3.left);

            if (aimInput == 1)
            {
                rotateDegrees += aimingSpeed * Time.deltaTime;
            }

            else if (aimInput == -1)
            {
                rotateDegrees -= aimingSpeed * Time.deltaTime;
            }

            crosshair.SendMessage("RotateCrosshair", rotateDegrees);
        }
    }
}
