using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour {

    public bool movePlayer;
    public GameObject spawnObject;
    GameObject player2;
    bool hasHappened = false;
    float speed = 17;
    bool playerMoved = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!hasHappened)
            {
                hasHappened = true;
                Instantiate(spawnObject);
                if (movePlayer)
                    player2 = GameObject.FindGameObjectWithTag("Player2");
            }
        }       
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!playerMoved && hasHappened && movePlayer)
        {
            if (player2.transform.position.x != 115)
            {
                float step = speed * Time.deltaTime;
                player2.transform.position = Vector3.MoveTowards(player2.transform.position, new Vector3(115, -14.6f, 0), step);
            }
            else
                playerMoved = true;
        }
    }
}
