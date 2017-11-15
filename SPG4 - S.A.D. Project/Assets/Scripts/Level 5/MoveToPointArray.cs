using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPointArray : MonoBehaviour {

    [Tooltip("Where the object get its points from.")]
    public Transform waypointContainer;
    [Tooltip("How fast the object moves.")]
    public float speed;

    private Transform[] waypoints;
    private int numberOfChildren = 0;

    public static bool puzzleDone = false;
    private bool canMove = true;
    private Vector2 currentPos;
    private Vector2 endPos;

    // Use this for initialization
    void Start()
    {
        puzzleDone = false;
        
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
            if (canMove == true)
            {
                currentPos = this.gameObject.transform.position;                             // Saves platforms position
                this.gameObject.transform.position = Vector3.MoveTowards(currentPos, endPos, Time.deltaTime * speed); // Moves platform

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
            endPos = waypoints[++numberOfChildren].transform.position;
            canMove = true;
        }
        else if (canMove == false && puzzleDone == true)
        {
            endPos = waypoints[++numberOfChildren].transform.position;
            canMove = true;
            puzzleDone = false;
        }
    }
}
