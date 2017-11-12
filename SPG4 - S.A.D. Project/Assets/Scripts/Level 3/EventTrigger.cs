using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour {

    public GameObject spawnObject;
    GameObject player;
    bool hasHappened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!hasHappened)
            {
                hasHappened = true;
                // Debug.Log(collision.gameObject);
                GameObject spawnedObject = Instantiate(spawnObject);
                player = collision.transform.root.gameObject;
                Debug.Log(player.tag);
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, -0.5f)* 5000);
            }
        }       
    }
}
