// Maximilian Törn Almö
// 2017-10-16

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointToPoint : MonoBehaviour {

    public GameObject toPoint;
    public float time;
    public float maxTime = 5;
    public float resetTime = 0;

    private GameObject moveObject;
    public Rigidbody2D parent;
    private bool moveBack = false;
    private float currentTime;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 direction;
    public Vector2 velocity;
    public float speed = 2f;
    private float originalSpeed = 2f;

    private bool moving;
	// Use this for initialization
	void Start () {
        moveObject = this.gameObject;
        startPos = moveObject.transform.position;
        endPos = toPoint.transform.position;
        direction = endPos - startPos;
        direction.Normalize();
        //parent = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            moving = true;
            currentTime += Time.deltaTime;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        moving = false;
    }

    /// <summary>
    /// Moves object back and furth from point A to B
    /// </summary>
    // Update is called once per frame
    void Update ()
    {
        if (moving)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            if (currentTime > maxTime)
            {
                currentTime = resetTime;
                direction.x *= -1;
                direction.y *= -1;
                gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
            }
            velocity.x = speed * direction.x;
            velocity.y = speed * direction.y;
            gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
        }
        else if (!moving)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        //if (currentTime >= time)
        //{
        //    currentTime = time;
        //    direction.x *= -1;
        //}
        //float Perc = currentTime / time;

        //velocity.y = moveObject.GetComponent<Rigidbody2D>().velocity.y;


        //parent.velocity = velocity;

        //Debug.Log(velocity);

        //if (moveBack == false)
        //{      
        //moveObject.transform.position = Vector3.Lerp(startPos, endPos, Perc);
        //}
        //else
        //{
        //moveObject.transform.position = Vector3.Lerp(endPos, startPos, Perc);
        //}

        //Debug.Log(velocity);

        //if (currentTime == time && moveBack == false)
        //{
        //    currentTime = 0;
        //    moveBack = true;
        //}else if (currentTime == time && moveBack == true)
        //{
        //    currentTime = 0;
        //    moveBack = false;
        //}
    }
}
