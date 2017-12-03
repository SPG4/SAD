using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTAM : MonoBehaviour
{
    public GameObject tam;
    //TAM tam = new TAM();
    // Use this for initialization
    void Start()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectiles"))

            
        {
            tam.gameObject.SendMessage("ApplyDamage");
            //tam.ApplyDamage();
            Destroy(collision.gameObject);
        }
    }
}
