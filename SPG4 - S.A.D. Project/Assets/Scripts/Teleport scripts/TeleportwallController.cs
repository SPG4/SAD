using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportwallController : MonoBehaviour {

    public bool isUsableOnce;

    private bool hasBeenUsed;
    private GameObject parent;
    private GameObject teleportBall;

    // Use this for initialization
    void Start ()
    {
        parent = this.gameObject.transform.parent.gameObject;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        teleportBall = collision.transform.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            if (isUsableOnce)
            {
                hasBeenUsed = true;
            }

            teleportBall.SendMessage("TeleportBallEvent");
        }
    }

    //private void OnTriggerExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
    //    {
    //        hasBeenUsed = true;
    //    }
    //}

	// Update is called once per frame
	void Update () {
		if (hasBeenUsed)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            parent.gameObject.layer = LayerMask.NameToLayer("Teleport destroy field");
        }

        Debug.Log(hasBeenUsed);
	}
}
