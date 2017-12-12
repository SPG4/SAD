using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleProjectile : MonoBehaviour
{

    public GameObject teleToThis;
    public GameObject pl1;
    public GameObject pl2;
    public GameObject teleBall;

    public GameObject projectile;

    // Use this for initialization
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ProjectileTAM")
        {
            collision.gameObject.transform.position = teleBall.transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
