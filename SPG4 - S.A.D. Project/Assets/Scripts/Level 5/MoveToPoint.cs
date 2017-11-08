using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour {
    public GameObject toPoint;
    public float speed;

    private Vector2 currentPos;
    private GameObject moveObject;
    private bool moveBack = false;
    private float currentTime;
    private Vector2 startPos;
    private Vector2 endPos;
    
	// Use this for initialization
	void Start () {
        moveObject = this.gameObject;
        startPos = moveObject.transform.position;
        endPos = toPoint.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        if (moveBack == false)
        {
            currentPos = this.gameObject.transform.position;                             // Saves platforms position
            this.gameObject.transform.position = Vector3.MoveTowards(currentPos, endPos, Time.deltaTime * speed);
        }
        else
        {
            currentPos = this.gameObject.transform.position;                             // Saves platforms position
            this.gameObject.transform.position = Vector3.MoveTowards(currentPos, startPos, Time.deltaTime * speed);
        }
        if(currentPos == endPos)
        {
            moveBack = true;
        } else if(currentPos == startPos) {
            moveBack = false;
        }
    }
}
