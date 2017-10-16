// Maximilian Törn Almö
// 2017-10-16

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointToPoint : MonoBehaviour {

    public GameObject toPoint;
    public float time;

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

	/// <summary>
    /// Moves object back and furth from point A to B
    /// </summary>
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if (currentTime >= time)
        {
            currentTime = time;
        }
        float Perc = currentTime / time;

        if(moveBack == false)
        {
            moveObject.transform.position = Vector3.Lerp(startPos, endPos, Perc);
        }
        else
        {
            moveObject.transform.position = Vector3.Lerp(endPos, startPos, Perc);
        }

        if (currentTime == time && moveBack == false)
        {
            currentTime = 0;
            moveBack = true;
        }else if (currentTime == time && moveBack == true)
        {
            currentTime = 0;
            moveBack = false;
        }
    }
}
