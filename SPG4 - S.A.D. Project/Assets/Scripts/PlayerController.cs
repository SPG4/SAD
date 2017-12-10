using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    //Public fields
    public int playerNumber;
    public string activeAbility;

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
    public Transform BallSpawnPoint;

    public PlayerController otherPlayer;

    public Animator bodyAnimator;
    public GameObject DialogueBox;

    public AudioClip jumpSound;

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
    private bool hasJumped;
    private bool insideAntigravArea;

    [SerializeField]
    private int energy;
    private float horizontalInput;
    private float aimInput;
    private float aimingSpeed;
    private float worldHalfSize;
    private float fanPushForce = 200;
    private float blendIdle = 0;
    private float blendSpeed = 3.0f;
    private float blendMax = 100.0f;
    private Vector2 velocity;
    private Vector3 initialVector = Vector3.forward;
    private Vector3 worldSize;

    private Rigidbody2D ridgidbodyPlayer;
    private GameObject defaultCollider;
    private GameObject crouchCollider;
    private Transform blackHoleCheckPoint;

    private PlayerAbilities playerAbilities;
    private EnergyButtonController energyBtn;


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

        aimingSpeed = 90f;

        worldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        worldHalfSize = this.GetComponentInChildren<Renderer>().bounds.size.x / 2;

        playerAbilities = gameObject.GetComponent<PlayerAbilities>();

        blackHoleCheckPoint = GameObject.Find("StartPoint").GetComponent<Transform>();

        //Debug.Log(blackHoleCheckPoint);

        //BallSpawnPoint = transform.Find("BallSpawnPoint");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Energy button")
        {
            energyBtn = collision.gameObject.GetComponent<EnergyButtonController>();
            if (energyBtn.hasBeenTriggered == false && energy > 0)
            {
                energyBtn.SendMessage("PlayeTeleportSound");
                energyBtn.hasBeenTriggered = true;
                DecreaseEnergy(1);
                otherPlayer.SendMessage("DecreaseEnergy", 1);
            }
        }

        if (collision.gameObject.tag == "Death trigger")
        {
            gameObject.transform.position = blackHoleCheckPoint.transform.position;
            otherPlayer.transform.position = blackHoleCheckPoint.transform.position;
            Debug.Log(blackHoleCheckPoint);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Teleport antigrav field")
        {
            insideAntigravArea = true;
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Fan trigger area"))
        {
            if (collision.GetComponent<FanController>().IsFanOn())
                AffectedByFan(fanPushForce);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Teleport antigrav field")
        {
            insideAntigravArea = false;
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
        if (activeAbility == "Shield")
        {
            bodyAnimator.SetLayerWeight(1, 0);
            crosshair.SetActive(false);
        }
        else
        {
            bodyAnimator.SetLayerWeight(1, 1);
            crosshair.SetActive(true);
        }

        aimInput = Input.GetAxis("Aim" + playerNumber);

        /*
          Set player bools grounded and isOnWall based on if the Ground Check and Wall Check transforms
          (which are placed on the player) are overlaping the LayerMask that specifies what counts as a
          wall or ground.
        */
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isOnWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        bodyAnimator.SetBool("Ground", grounded);

        bodyAnimator.SetBool("IsOnWall", isOnWall);
        bodyAnimator.SetBool("DoubleJumped", hasDoubleJumped);
        bodyAnimator.SetFloat("vSpeed", ridgidbodyPlayer.velocity.y);
        bodyAnimator.SetBool("hasJumped", hasJumped);

        bodyAnimator.SetFloat("xSpeed", Mathf.Abs(horizontalInput));

        oldJumpState = jumpState;
        jumpState = Input.GetButton("Jump" + playerNumber);

        CheckSpeedLimit();

        bodyAnimator.SetFloat("AimInput", blendIdle);

        if (aimInput < 0)
        {
            bodyAnimator.SetFloat("AimInput", blendIdle -= blendSpeed);
        }
        if (aimInput > 0)
        {
            bodyAnimator.SetFloat("AimInput", blendIdle += blendSpeed);
        }

        if (blendIdle < -blendMax) blendIdle = -blendMax;
        if (blendIdle > blendMax) blendIdle = blendMax;

        //Walljump is ended when on ground and ability to doublejump is reset
        if (grounded)
        {
            hasJumped = false;
            hasDoubleJumped = false;
            wallJumping = false;
        }

        //Dont allow the player to turn or move during a walljump
        if (!wallJumping && !DialogueBox.activeInHierarchy)
        {
            TurnToInputDirection();
            Movement();
        }
        else if (DialogueBox.activeInHierarchy)
            ridgidbodyPlayer.velocity = Vector2.zero;

        if (wallJumped)
        {
            WallJump();
        }

        //Check if a player has pressed the jump button and is standing on ground
        if (jumpState && !oldJumpState && grounded)
        {
            Jump();
        }

        ////Check if a player is pressing jump in the air after the player has jumped once
        if (jumpState && !oldJumpState && !grounded && !hasDoubleJumped && !isOnWall)
        {
            Jump();
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
        SoundLevelManager.Instance.PlaySingle(jumpSound);
        hasJumped = true;
        ridgidbodyPlayer.velocity = new Vector2(ridgidbodyPlayer.velocity.x, 0);
        velocity = ridgidbodyPlayer.velocity;
        ridgidbodyPlayer.AddForce(Vector2.up * jumpForce);
    }

    public void WallJump()
    {
        SoundLevelManager.Instance.PlaySingle(jumpSound);
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
        Vector2 shootingDirection = crosshair.transform.position - BallSpawnPoint.transform.position;
        shootingDirection.Normalize();

        Debug.Log(shootingDirection);

        float speed = 15;

        shooting = true;

        if (insideAntigravArea == false)
        {
            GameObject ball = Instantiate(teleportBall, BallSpawnPoint.position, BallSpawnPoint.rotation);
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

    private void AffectedByFan(float fanPushForce)
    {
        //ridgidbodyPlayer.gravityScale = 0;
        ridgidbodyPlayer.AddForce(new Vector2(0, fanPushForce));
    }

    private void IncreaseEnergy(int amount)
    {
        energy += amount;
    }

    private void DecreaseEnergy(int amount)
    {
        energy -= amount;
    }

    private void SetActiveAbility(string value)
    {
        activeAbility = value;
    }

    private void SetBlackHoleCheckPoint(GameObject blackHole)
    {
        Transform blackHoleExit;
        blackHoleExit = blackHole.transform.Find("Black hole exit");
        blackHoleCheckPoint = blackHoleExit;
    }

    public bool InsideAntiGravArea()
    {
        return insideAntigravArea;
    }
}
