﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideTAM : MonoBehaviour {

    public GameObject exit;
    public float time;

    public GameObject ply1;
    public GameObject ply2;

    private bool startTimer = false;
    float orgTime;
	// Use this for initialization
	void Start () {
        orgTime = time;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") || 
            collision.gameObject.layer == LayerMask.NameToLayer("Projectiles"))
        {
            startTimer = true;
            if (time <= 0)
            {
                moveObjects(collision.gameObject);
                time = orgTime;
            }
        }
        else
        {
            startTimer = false;
        }
    }

    void Update()
    {
        if (startTimer == true)
        {
            time -= Time.deltaTime;
        }

    }

    void moveObjects(GameObject obj)
    {
        ply1.gameObject.transform.position = exit.transform.position;
        ply2.gameObject.transform.position = exit.transform.position;
        obj.gameObject.transform.position = exit.transform.position;
    }
}