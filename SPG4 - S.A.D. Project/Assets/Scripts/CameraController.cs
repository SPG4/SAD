using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject playerOne;
    GameObject playerTwo;

    float playerDistance;

    // Use this for initialization
    void Start ()
    {
        playerOne = GameObject.FindGameObjectWithTag("Player");
        playerTwo = GameObject.FindGameObjectWithTag("Player2");
	}

    // Update is called once per frame
    void Update ()
    {
        /*Dont move camera if players are a certain distance apart, still need to stop players from 
        //being able to move outside camera

        //playerDistance = (playerOne.transform.position - playerTwo.transform.position).magnitude;
        if (playerDistance < 15)*/
            //Call FixedCameraFollowSmooth

        FixedCameraFollowSmooth(gameObject.GetComponent<Camera>(), playerOne.transform, playerTwo.transform);
    }


    public void FixedCameraFollowSmooth(Camera cam, Transform t1, Transform t2)
    {
        // How many units should we keep from the players
        float followTimeDelta = 0.8f;

        // Midpoint we're after
        Vector3 midpoint = (t1.position + t2.position) / 2f;

        // Distance between objects
        float distance = (t1.position - t2.position).magnitude;

        // Move camera a certain distance
        Vector3 cameraDestination = midpoint - cam.transform.forward;

        // Adjust ortho size if we're using one of those
        if (cam.orthographic)
        {
            //Use this if you want the camera to zoom based on distance between players

            //if (distance < 5)
            //    cam.orthographicSize = 5;
            //else if (distance >= 5 && distance < 10)
            //    cam.orthographicSize = distance;
            //else if (distance > 10)
            //    cam.orthographicSize = 10;
        }
        
        cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

        // Snap when close enough to prevent annoying slerp behavior
        if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
            cam.transform.position = cameraDestination;

        //Debug.Log(cam.transform.position);
    }
}
