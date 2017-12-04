using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthTAM : MonoBehaviour
{
    public GameObject teleToThis;
    public GameObject pl1;
    public GameObject pl2;
    public GameObject teleBall;

    public GameObject projectile;
    public static bool isDamaged = false;
    // Use this for initialization
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged == false)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Ball") || collision.gameObject.tag == "ProjectileTAM")
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
                {
                    collision.gameObject.transform.position = teleBall.transform.position;
                }
                if(collision.gameObject.tag == "ProjectileTAM")
                {
                    projectile.gameObject.SendMessage("EnterTAM");
                    collision.gameObject.transform.position = teleBall.transform.position;
                }
                TelePlayers();
            }
            if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
            {
                collision.transform.position = teleToThis.transform.position;
            }
        }
    }

    void TelePlayers()
    {
        pl1.transform.position = teleToThis.transform.position;
        pl2.transform.position = teleToThis.transform.position;
    }
}
