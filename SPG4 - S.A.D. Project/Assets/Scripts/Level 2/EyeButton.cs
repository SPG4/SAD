﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeButton : MonoBehaviour {

    public Sprite closedEye;
    public Sprite eyeWithBall;
    public GameObject platform;
    float timer = -1;

	void Start () {
		
	}
	

	void Update ()
    {
		if (timer >= 0 )
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = closedEye;

            timer -= Time.deltaTime;
        }
        if (timer > -1 && timer < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = eyeWithBall;
            platform.SendMessage("StartMove");
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EyeBall")
        {
            timer = 1;
            Destroy(collision.gameObject);
        }
    }
}
