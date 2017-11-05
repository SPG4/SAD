using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public Vector2 direction; //y or x movement
    public float speed;
    public float timer;
    float time;
    float movementSpeed;
    Vector2 startpos;

	void Start ()
    {
        //timer = Random.Range(0, movementTimeLimit);
        time = timer;
        movementSpeed = Random.Range(speed - 0.1f, speed + 0.1f);
        startpos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

    }
	
	void Update ()
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
