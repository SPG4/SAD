using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    public float ballSpeed = 10f;
    public float originalBallSpeed = 10f;
    public string playerShootingString;
    public string playerBeingTeleportedString;

    GameObject playerBeingTeleported;
    GameObject playerShooting;
    BlackHoleController blackHole;
    Vector2 playerShootingLocalScale;
    Vector2 ballDirection;

    public LayerMask whatIsTeleportableSurface;
    public float hitRadius = 0.3f;
    public bool touchingTeleportableSurface;

    public LayerMask whatIsTeleportDestroySurface;
    public bool touchingTeleportDestroySurface;

    public Transform hitCheck; 

    private float timer;
    private bool insideAntigravField;

    void Start()
    {
        timer = 3;

        //Reset ballspeed to the original direction;
        ballSpeed = originalBallSpeed;

        //Get the facing direction of the player that shot the ball
        playerShootingLocalScale = GameObject.FindGameObjectWithTag(playerShootingString).GetComponent<PlayerController>().transform.localScale;
        //blackHole = GameObject.FindGameObjectWithTag("Black hole").GetComponent<BlackHoleController>();

        //Get the player that is being teleported
        playerBeingTeleported = GameObject.FindGameObjectWithTag(playerBeingTeleportedString);
        playerShooting = GameObject.FindGameObjectWithTag(playerShootingString);

        //If shooting player is facing left, invert the speed of the ball
        if (playerShootingLocalScale.x < 0.1)
            ballSpeed = ballSpeed * -1;
    }

    private void Update()
    {
        Debug.Log(blackHole);
        if (insideAntigravField)
        {
            timer = 3;
        }
        else if (!insideAntigravField)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            TeleportBallEvent();
        }
    } 

    private void FixedUpdate()
    {
        //touchingTeleportableSurface = Physics2D.OverlapCircle(hitCheck.position, hitRadius, whatIsTeleportableSurface);
        //touchingTeleportDestroySurface = Physics2D.OverlapCircle(hitCheck.position, hitRadius, whatIsTeleportDestroySurface);
    }

    void AddSpeedToBall(Vector2 direction)
    {
        ballDirection = direction;
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y);
    }

    /// <summary>
    /// When the ball collides with the ground the ball is destroyed and a message is sent to the 
    /// player being teleported with the new position for that player. The shooting bool for
    /// the player that shot is also set to false
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Interactable Objects"))
        {
            TeleportBallEvent();
        }

        //if (touchingTeleportableSurface)
        //{
        //    TeleportBallEvent();
        //    ResetShootingVariables();
        //}

        //if (touchingTeleportDestroySurface)
        //{
        
        //    Destroy(gameObject);
        //    ResetShootingVariables();
        //}

        if (collision.gameObject.layer == LayerMask.NameToLayer("Black hole"))
        {
            blackHole = collision.transform.gameObject.GetComponent<BlackHoleController>();
            TeleportBothPlayers();
        }

        else if (collision.gameObject.tag == "Teleport destroyer")
        {
            Destroy(gameObject);
            ResetShootingVariables();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Teleport antigrav field")
        {
            insideAntigravField = true;
            AntiGravityBall();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Teleport antigrav field")
        {
            insideAntigravField = false;
            ReturnToNormal();
        }
    }

    private void ReturnToNormal()
    {
        gameObject.layer = LayerMask.NameToLayer("Ball");
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void AntiGravityBall()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable Objects");
        GetComponent<Rigidbody2D>().gravityScale = 0;

        Vector2 CurrentVelocity = GetComponent<Rigidbody2D>().velocity;
        CurrentVelocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x * 0.9f, GetComponent<Rigidbody2D>().velocity.y * 0.9f);

        GetComponent<Rigidbody2D>().velocity = CurrentVelocity;
    }

    private void TeleportBallEvent()
    {
        Destroy(gameObject);
        playerBeingTeleported.SendMessage("Teleport", gameObject.transform.position);
        ResetShootingVariables();
    }

    private void TeleportBothPlayers()
    {
        Destroy(gameObject);
        playerBeingTeleported.SendMessage("Teleport", blackHole.target.transform.position);
        playerShooting.SendMessage("Teleport", blackHole.target.transform.position);
        ResetShootingVariables();
    }

    private void ResetShootingVariables()
    {
        GameObject.FindGameObjectWithTag(playerShootingString).GetComponent<PlayerController>().SendMessage("ResetShootingValue", false);
        GameObject.FindGameObjectWithTag(playerShootingString).GetComponent<PlayerAbilities>().SendMessage("ResetShot", 1);
    }
}
