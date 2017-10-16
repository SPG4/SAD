// Maximilian Törn Almö
// 10-14

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float destroyTime;
    public bool isDeadly = true;

    // Use this for initialization

    /// <summary>
    /// Destroy projectile after X seconds.
    /// </summary>
    void Start()
    {

        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Destroy projectile on trigger contact.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Turret.destroyOnTriggerContant == true)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// If isDeadlySet = true, deactivates player hit on contact.
    /// If destryOnTriggerContant = true, destroy projectile on collision.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Turret.isDeadlySet == true)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                GameObject.Find(collision.gameObject.name).SetActive(false);
            }
        }
        if(Turret.destroyOnTriggerContant == true)
        {
            Destroy(this.gameObject);
        }
        
    }
}
