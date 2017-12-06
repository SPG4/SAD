using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public Vector2 direction; //y or x movement
    public float speed;
    public float timer;
    public bool wait;
    public bool notRando;
    float time;
    float movementSpeed;
    Vector2 startpos;
    public float startTime = -1;

	void Start ()
    {
        //timer = Random.Range(0, movementTimeLimit);
        time = timer;
        if (!notRando)
        {
            startTime = 1;
            startTime = Random.Range(startTime - 0.2f, startTime + 0.2f);
        }

        movementSpeed = speed;
        startpos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

    }
	
	void Update ()
    {
        if (startTime > 0)
        {
            startTime -= Time.deltaTime;
        }

        if (!wait && startTime <= 0)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * movementSpeed;
            }
            else
            {
                if (new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) == startpos)
                {
                    time = timer;
                }
                else
                    gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * -movementSpeed;

            }
        }
    }

    public void StartMove()
    {
        wait = false;
    }
}
