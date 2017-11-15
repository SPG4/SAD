using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour {
    public static bool startPlattform;
    public bool delayOnPlattform;
    public bool stopPlattform;
    public float delay = 0.0f;
    public bool buttonPressed;
   public int playerCount;

    public GameObject moveWith;


	// Use this for initialization
	void Start () {
        playerCount = 0;
        startPlattform = false;
    }
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = moveWith.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ++playerCount;
            //collision.collider.transform.SetParent(transform);
        }
        if (playerCount >= 1)
        {
            startPlattform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            --playerCount;
            //collision.rigidbody.transform.SetParent(null);

        }
        if (playerCount == 0)
        {
            startPlattform = false;
        }
    }
    /// <summary>
    /// när player går in i triggerzonen så blir 
    /// plyercount +1. Om den är mer eller lika med 1 så åker är startplattform true.
    /// </summary>
    /// <param name="collision"></param>
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        ++playerCount;
    //    }

    //    if (playerCount >= 1)
    //    {
    //        startPlattform = true;
    //    }
       
    //}
    ///// <summary>
    ///// när player lämnar zonen så blir det startPlattform false om 
    ///// playercount = 0.
    ///// </summary>
    ///// <param name="collision"></param>
    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        --playerCount;
    //    }
        
    //    if (playerCount == 0)
    //    {
    //        startPlattform = false;
    //    }
    //}

    /// <summary>
    /// När man är i triggerzonen så räknar timern uppåt
    /// när den når 10 så blir delayOnPlattform true.
    /// Detta används för att ge en försening på plattfromen när det behövs.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerStay2D(Collider2D collision)
    {
        if (delay >= 0.0f)
        {
            //Debug.Log("hey");
            delay -= Time.deltaTime;
            delayOnPlattform = true;
        }
        
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    startPlattform = false;
    //    if (buttonPressed)
    //    {
    //        startPlattform = true;
    //    }
    //}


    //public void StopZone()
    //{
    //    if (stopPlattform == true)
    //    {
    //        startPlattform = false;
    //    }
    //}
}
