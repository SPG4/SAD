using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTimed : MonoBehaviour {
    public string objectNameToMove = "";
    public float speedX = 0.0f;
    public float speedY = 0.0f;
    [Tooltip("How long the object should move")]
    public float maxSeconds = 0.0f;
    [Tooltip("Time before button reset")]
    public float buttonTime = 0.0f;
    public bool isButton1 = false;
    public bool isButton2 = false;

    private float buttonOriginalTime;
    private bool startTime = false;
    private bool moveObject = false;
    // Use this for initialization
    void Start () {
        buttonOriginalTime = buttonTime;
        speedX /= 2;
        speedY /= 2;
	}

    private void FixedUpdate()
    {
        if (startTime == true)
        {
            checkButton();
        }
        if(moveObject == true)
        {
            move();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        startTime = true;
        if(isButton1 == true)
        {
            GlobalLevel1.Button1Clicked = true;
        }
        else if(isButton2 == true)
        {
            GlobalLevel1.Button2Clicked = true;
        }
    }

    private void checkButton()
    {
        buttonTime -= Time.fixedDeltaTime;

        if(GlobalLevel1.Button1Clicked == true && GlobalLevel1.Button2Clicked == true)
        {
            moveObject = true;
        }

        if (buttonTime <= 0)
        {
            startTime = false;
            buttonTime = buttonOriginalTime;

            if (isButton1 == true)
            {
                GlobalLevel1.Button1Clicked = false;
            }
            else if (isButton2 == true)
            {
                GlobalLevel1.Button2Clicked = false;
            }
        }

    }

    private void move()
    {
        GameObject.Find(objectNameToMove).transform.Translate(new Vector2(speedX, speedY) * Time.fixedDeltaTime);
        maxSeconds -= Time.fixedDeltaTime;

        if(maxSeconds <= 0)
        {
            moveObject = false;
        }
    }
}
