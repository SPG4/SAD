﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampButton : MonoBehaviour {

    public int buttonNr;
    Vector3 startPos;
    Vector3 pressedPos;
    bool pressed = false;
    bool unpress = false;
    public GameObject lamps;
    float timer;

	void Start ()
    {
        startPos = gameObject.GetComponent<Transform>().position;
        pressedPos = new Vector3(startPos.x, startPos.y - 0.20f, 0);

	}
	
	void Update ()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(gameObject.GetComponent<Transform>().position, pressedPos, 0.1f);
        }
        else
        {
            gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(gameObject.GetComponent<Transform>().position, startPos, 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pressed = true;
        timer = 1;

        if (buttonNr == 1)
        {
            lamps.transform.GetChild(0).SendMessage("Change2Red");
            lamps.transform.GetChild(1).SendMessage("Change2Green");
        }

        if (buttonNr == 2)
        {
            lamps.transform.GetChild(0).SendMessage("Change2Green");
        }

        if (buttonNr == 3)
        {
            lamps.transform.GetChild(1).SendMessage("Change2Red");
            lamps.transform.GetChild(2).SendMessage("Change2Green");
        }

    }
}