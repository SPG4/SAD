using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour {

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
                player2 = GameObject.FindGameObjectWithTag("Player2");
            }
        }       
    }

    private void Update()
    {
        if (!playerMoved && hasHappened)
        {
            if (player2.transform.position.x != 100)
            {
                float step = speed * Time.deltaTime;
                player2.transform.position = Vector3.MoveTowards(player2.transform.position, new Vector3(115, -14.6f, 0), step);
            }
            else
                playerMoved = true;
        }
    }
}
