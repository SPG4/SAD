// Maximilian Törn Almö
// 2017-10-03



//      *** Description ***
//  Script moves target object in X or Y.
//  You can choose speed in X or Y, for how long it should move,
//  if the button is already active from start,
//  if the button is a one time click or
//  if the object should move in oppesite directoion when not touching the button.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnOff : MonoBehaviour
{


    public string objectNameToMove = "";
    public float speedX = 0.0f;
    public float speedY = 0.0f;
    [Tooltip("How long the object should move")]
    public float maxSeconds = 0.0f;
    [Tooltip("If the button is active on start or not")]
    public bool buttonOn = false;
    //public bool showDebug = false;
    [Tooltip("If the button is only clickable once")]
    public bool oneTimeClick = false;

    public Sprite changeToTexture = null;
    public GameObject whenObjectTouches = null;

    private float chosenSeconds = 0.0f;
    private bool notClickable = false;
    // Use this for initialization
    void Start()
    {
        chosenSeconds = maxSeconds;
    }

    private void FixedUpdate()
    {
        moveObject();
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (whenObjectTouches != null && changeToTexture != null)
        {
            if (collision.transform.gameObject.name == whenObjectTouches.name)
            {
                this.gameObject.GetComponentInParent<SpriteRenderer>().sprite = changeToTexture;
                Destroy(whenObjectTouches);
            }
        }
        if (oneTimeClick == false)
        {
            buttonOn = true;
        }
        else if (oneTimeClick == true && notClickable == false)
        {
            buttonOn = true;
            notClickable = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(notClickable == false)
        {
            buttonOn = false;
        }
        
    }

    /// <summary>
    /// Move object while object is inside object or not. Moves both ways.
    /// </summary>
    private void moveObject()
    {
        if (buttonOn == true && 0 < maxSeconds)
        {
            GameObject.Find(objectNameToMove).transform.Translate(new Vector2(speedX, speedY) * Time.fixedDeltaTime);
            maxSeconds -= Time.fixedDeltaTime;

            //if (showDebug == true) { Debug.Log(maxSeconds); }

        }
        else if (buttonOn == false && maxSeconds < chosenSeconds)
        {
            GameObject.Find(objectNameToMove).transform.Translate(new Vector2(-1 * speedX, -1 * speedY) * Time.fixedDeltaTime);
            maxSeconds += Time.fixedDeltaTime;
        }
    }

}
