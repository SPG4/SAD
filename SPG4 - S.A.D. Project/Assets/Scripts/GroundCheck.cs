using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public PlayerController player;

    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerController>();
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        player.grounded = true;
    //    }
    //}
    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        player.grounded = true;
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        player.grounded = false;
    //    }
    //}
}
