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
    Vector2 playerShootingLocalScale;

    private float timer;

    void Start()
    {
        timer = 3;

        //Reset ballspeed to the original direction;
        ballSpeed = originalBallSpeed;

        //Get the facing direction of the player that shot the ball
        playerShootingLocalScale = GameObject.FindGameObjectWithTag(playerShootingString).GetComponent<PlayerController>().transform.localScale;

        //Get the player that is being teleported
        playerBeingTeleported = GameObject.FindGameObjectWithTag(playerBeingTeleportedString);

        //If shooting player is facing left, invert the speed of the ball
        if (playerShootingLocalScale.x < 0.1)
            ballSpeed = ballSpeed * -1;

        //Add speed to the ball

        //GetComponent<Rigidbody2D>().velocity = new Vector2(ballSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            TeleportBallEvent();
        }
        
    }

    void AddSpeedToBall(Vector2 direction)
    {
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
        if (collision.gameObject.tag == "Ground")
        {
            TeleportBallEvent();
        }
    }

    private void TeleportBallEvent()
    {
        Destroy(gameObject);
        playerBeingTeleported.SendMessage("Teleport", gameObject.transform.position);
        GameObject.FindGameObjectWithTag(playerShootingString).GetComponent<PlayerController>().shooting = false;
        GameObject.FindGameObjectWithTag(playerShootingString).GetComponent<PlayerAbilities>().shots = 1;
    }
}
