using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl5Puzzle4 : MonoBehaviour {
    public GameObject obj1;
    public GameObject obj2;

    public float resetTime;
    private float orgTime;

    private bool startTime;
    private bool clickable;
    private bool clicked;
	// Use this for initialization
	void Start () {
        clicked = false;
        orgTime = resetTime;
        startTime = false;
        clickable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(clickable == true)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                resetTime = orgTime;
                startTime = false;
                CloseEye();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            resetTime = orgTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (true)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                startTime = true;
                resetTime = orgTime;
            }
        }
    }

    private void Update()
    {
        if (resetTime >= 0 && startTime == true)
        {
            resetTime -= Time.deltaTime;
        }
        else if (resetTime <= 0)
        {
            startTime = false;
            clickable = true;

            OpenEye();
        }
    }


    private void OpenEye()
    {
        obj2.SetActive(false);
        obj1.SetActive(true);
        Lvl5Puzzel4Turret.canFire = false;

        
        //resetTime = orgTime;
        //startTime = true;
    }

    private void CloseEye()
    {
        obj1.SetActive(false);
        obj2.SetActive(true);
        Lvl5Puzzel4Turret.canFire = true;

        clickable = false;
        // Play squish sound
    }
}
