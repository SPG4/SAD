using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTAM : MonoBehaviour
{
    public GameObject tam;
    public GameObject tamTime;
    //TAM tam = new TAM();
    // Use this for initialization
    void Start()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable Objects"))  
        {
            tam.gameObject.SendMessage("ApplyDamage");
            tamTime.gameObject.SendMessage("NoTime");
            MouthTAM.isDamaged = true;
            Destroy(collision.gameObject);
        }
    }
}
