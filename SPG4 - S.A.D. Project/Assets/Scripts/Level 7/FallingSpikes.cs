using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    public float spikeTime;
    public bool falling;
    public bool fallTime;
    bool isHit;
    Rigidbody2D rb;
    public GameObject obj;
    GameObject spike;

    void Start()
    {
        fallTime = false;
        isHit = false;
    }

    void Update()
    {
        if(fallTime == true)
        {
            Spikefall();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ProjectileTAM")
        {
            isHit = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            if(isHit == true)
            {
                fallTime = true;
            }
        }

        //If the spike touches the ground, it will be destroyed and a new spike is instantiated
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (falling)
            {
                Destroy(gameObject);
                spike = Instantiate(Resources.Load("Spike", typeof(GameObject))) as GameObject;
            }
        }
    }

    void Spikefall()
    {
        if (fallTime == true)
        {
            spikeTime -= Time.deltaTime;
        }
        if (spikeTime <= 0.0f)
        {
            falling = true;
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
