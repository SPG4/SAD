using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drunkard : MonoBehaviour {

    float timer = 3;
    float time;
    bool awake = false;
    bool hasMoved = false;
    float movementSpeed = 0.05f;
    Vector2 direction;
    Vector2 startpos;
    public GameObject coke;

    private void Start()
    {
        time = timer;
        direction = new Vector2(0, 1);
        startpos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    void Update()
    {
        if (awake)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * movementSpeed; 
                hasMoved = true;
            }
            else
            {
                if (new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) == startpos && hasMoved)
                {
                    time = timer;
                    awake = false;
                    hasMoved = false;
                    Instantiate(coke);
                }
                else
                    gameObject.transform.position += new Vector3(direction.x, direction.y, 0) * -movementSpeed;
            }
        }

    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable Objects"))
        {
            awake = true;
            Destroy(collision.gameObject);
        }
    }
}
