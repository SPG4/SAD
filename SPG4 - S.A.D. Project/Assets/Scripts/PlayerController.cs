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
    [SerializeField]
    private float wallJumpForce = 20f;
    [SerializeField]
    private float jumpPushForce = 40f;

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
    private bool cancelJump;
    private bool grounded;
    private bool hasDoubleJumped;
    private bool isOnWall;
    private bool crouchState;
    private bool facingRight;
    private bool shooting;
    private bool wallJumped = false;
    private bool wallJumping = false;
    private bool insideAntigravArea;

    private float horizontalInput;
    private float aimInput;
    private float aimingSpeed;
    private float worldHalfSize;
    private Vector2 velocity;
    private Vector3 initialVector = Vector3.forward;
    private Vector3 worldSize;

    private Rigidbody2D ridgidbodyPlayer;
    private GameObject defaultCollider;
    private GameObject crouchCollider;

    private PlayerAbilities playerAbilities;

    private AudioSource jumping;

    private float mudFloorJumpForce = 1000f;
    private bool standingOnMudFloor;
    private bool mudFloorJumping;

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

        playerAbilities = gameObject.GetComponent<PlayerAbilities>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Teleport antigrav field")
        {
            insideAntigravArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Teleport antigrav field")
        {
            insideAntigravArea = false;
            shooting = false;
            playerAbilities.SendMessage("ResetShot", 1);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Mud floor"))
        {
            standingOnMudFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Mud floor"))
        {
            standingOnMudFloor = false;
        }
    }

    /// <summary>
    /// Update is called once a frame
    /// </summary>
    void Update ()
    {
        aimInput = Input.GetAxis("Aim" + playerNumber);     
        cancelJump = Input.GetButtonUp("Jump" + playerNumber);
        crouchState = Input.GetButton("Crouch" + playerNumber);
        horizontalInput = Input.GetAxis("Horizontal" + playerNumber);

        AimCrosshairRelativeToPlayer(aimInput);

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

        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump" + playerNumber);

        CheckSpeedLimit();

        //Walljump is ended when on ground and ability to doublejump is reset
        if (grounded)
        {
            hasDoubleJumped = false;
            wallJumping = false;
            mudFloorJumping = false;
        }

        //Dont allow the player to turn or move during a walljump
        if (!wallJumping)
        {
            TurnToInputDirection();
            Movement();
        }

        if (wallJumped)
        {
            WallJump();
        }

        //Check if a player has pressed the jump button and is standing on ground
        if (jumpState && !oldJumpState && grounded && !standingOnMudFloor)
        {
            Jump();
        }

        if (jumpState && !oldJumpState && grounded && standingOnMudFloor)
        {
            MudFloorJump();
        }

        ////Check if a player is pressing jump in the air after the player has jumped once
        if (jumpState && !oldJumpState && !grounded && !hasDoubleJumped && !isOnWall && !mudFloorJumping)
        {
            Jump();
            hasDoubleJumped = true;
        }

        if (jumpState && !oldJumpState && !grounded && !hasDoubleJumped && !isOnWall && mudFloorJumping)
        {
            MudFloorJump();
            hasDoubleJumped = true;
        }

        //Check if a player is up against a wall, if so it counts as staning on the ground
        if (jumpState && !oldJumpState && isOnWall && !grounded) //|| standingOnObject)
        {
            wallJumped = true; 
            hasDoubleJumped = true;
        }

        //Used to cancel a players jump if they release the jump button before reaching the pivot point
        if (cancelJump)
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * .5f;
                ridgidbodyPlayer.velocity = new Vector2(ridgidbodyPlayer.velocity.x, velocity.y);
            }
        }

        //Make players fall slower when on walls to create a gliding effect, making it easier to walljmp
        if (isOnWall)
        {
             if (ridgidbodyPlayer.velocity.y < 0)
            {
                ridgidbodyPlayer.velocity = new Vector2(0, ridgidbodyPlayer.velocity.y * 0.5f);
            }
        }
    }

    /// <summary>
    /// Gets the input value from the axis with the name Horizontal plus the players number. 
    /// The value is then multiplied with specified speed values that correspond to different
    /// states that the player is in, for example crouching and standing normally
    /// </summary>
    private void Movement()
    {
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

        //worldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        //Vector3 worldLeft = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));

        //if (ridgidbodyPlayer.position.x >= worldSize.x - worldHalfSize)
        //{
        //    ridgidbodyPlayer.position = new Vector2(worldSize.x - worldHalfSize, ridgidbodyPlayer.position.y);
        //    Debug.Log("out of bounds");
        //}
        //else if (ridgidbodyPlayer.position.x <= (worldLeft.x + worldHalfSize))
        //{
        //    ridgidbodyPlayer.position = new Vector2(worldLeft.x + worldHalfSize, ridgidbodyPlayer.position.y);
        //} 
    }

    private void LateUpdate()
    {
        CheckPlayersInsideCamera();
    }

    void CheckPlayersInsideCamera()
    {
        
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

    public void MudFloorJump()
    {
        jumping.Play();
        mudFloorJumping = true;
        ridgidbodyPlayer.velocity = new Vector2(ridgidbodyPlayer.velocity.x, 0);
        velocity = ridgidbodyPlayer.velocity;
        ridgidbodyPlayer.AddForce(Vector2.up * mudFloorJumpForce);
    }

    public void WallJump()
    {
        jumping.Play();
        ridgidbodyPlayer.velocity = new Vector2(jumpPushForce * (facingRight ? -1 : 1), wallJumpForce);
        wallJumping = true;
        Flip();
        wallJumped = false;
    }

    /// <summary>
    /// Checks if the velocity of the players ridgidbody exceeds the maxSpeed value in both
    /// the positive and negative X-axis, if so the velocity is set to the positive or negative
    /// maxSpeed value respectively
    /// </summary>
    private void CheckSpeedLimit()
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

        if (insideAntigravArea == false)
        {
            GameObject ball = Instantiate(teleportBall, wallCheck.position, wallCheck.rotation);
            ball.GetComponent<ShootBall>().playerShootingString = gameObject.tag;
            ball.GetComponent<ShootBall>().playerBeingTeleportedString = otherPlayer.tag;
            ball.SendMessage("AddSpeedToBall", shootingDirection * speed);
        }
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
