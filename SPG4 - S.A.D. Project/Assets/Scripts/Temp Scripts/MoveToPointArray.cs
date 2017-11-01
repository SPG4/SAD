using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPointArray : MonoBehaviour {

    public Transform waypointContainer;
    public float time;
    private float originTime;

    private Transform[] waypoints;
    private int numberOfChildren = 0;

    private GameObject moveObject;
    private bool canMove = true;
    public static bool puzzleDone = false;
    private float currentTime;

    private Vector2 currentPos;
    private Vector2 startPos;
    private Vector2 endPos;

    // Use this for initialization
    void Start()
    {
        moveObject = this.gameObject;
        startPos = moveObject.transform.position;
        originTime = time;

        waypoints = new Transform[waypointContainer.childCount];
        for (int i = 0; i < waypointContainer.childCount; i++)
        {
            waypoints[i] = waypointContainer.transform.GetChild(i);
            ++numberOfChildren;
        }
        numberOfChildren = 0;
        endPos = waypoints[numberOfChildren].transform.position;
    }

    /// <summary>
    /// Moves object back and furth from point A to B
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        if (puzzleDone == true) {
            canMove = true;
        }

        if (MovePlatform.startPlattform == true)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= time)
            {
                currentTime = time;
            }
            float Perc = currentTime / time;
            if (canMove == true)
            {
                moveObject.transform.position = Vector3.Lerp(startPos, endPos, Perc);   // Move object to next position

                currentPos = moveObject.transform.position;                             // Saves platforms position
                if (currentPos == endPos && waypoints[numberOfChildren].gameObject.name.Contains("!")) // if the platform position is on the end position,
                {
                    canMove = false;
                    moveToNextPoint();
                }
                else if (currentPos == endPos)
                {
                    canMove = false;
                    moveToNextPoint();
                }
            }
        }
    }

    private void moveToNextPoint()
    {
        if (canMove == false && !waypoints[numberOfChildren].gameObject.name.Contains("!"))
        {
            startPos = currentPos;
            endPos = waypoints[++numberOfChildren].transform.position;
            currentTime = 0;
            time = originTime;
            canMove = true;
        }
        else if (canMove == false && puzzleDone == true)
        {
            startPos = currentPos;
            endPos = waypoints[++numberOfChildren].transform.position;
            currentTime = 0;
            time = originTime;
            canMove = true;
            puzzleDone = false;
        }
    }
}
