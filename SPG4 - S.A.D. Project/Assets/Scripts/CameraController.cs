using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerOne, playerTwo;
    public float minSizeY = 4f;
    public float maxSizeY = 7f;
    public float dampTime = 0.15f;
    private Vector3 middle, point, delta, destination;
    private Vector3 velocity = Vector3.zero;

    void SetCameraPos()
    {
        middle = (playerOne.position + playerTwo.position) * 0.5f;
        point = GetComponent<Camera>().WorldToViewportPoint(middle);
        delta = middle - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

    }

    void SetCameraSize()
    {
        //horizontal size is based on actual screen ratio
        float minSizeX = minSizeY * Screen.width / Screen.height;
        float screenRatio = Screen.width / Screen.height;

        //Get magnitude
        float xDiff = (playerOne.position - playerTwo.position).magnitude;

        //multiplying by 0.5, because the ortographicSize is actually half the height
        float width = Mathf.Abs((playerOne.position - playerTwo.position).magnitude - 1f) * screenRatio * 0.5f;
        float height = Mathf.Abs((playerOne.position - playerTwo.position).magnitude - 1f) * 0.5f;

        //computing the size
        float camSizeX = Mathf.Max(width, minSizeX);
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(Mathf.Max(height, camSizeX * Screen.height / Screen.width, minSizeY), minSizeY, maxSizeY);
    }
    private void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("Player").transform;
        playerTwo = GameObject.FindGameObjectWithTag("Player2").transform;
    }
    void Update()
    {
        SetCameraPos();
        SetCameraSize();
    }

    //OLD

    //   GameObject playerOne;
    //   GameObject playerTwo;

    //   float playerDistance;

    //   // Use this for initialization
    //   void Start ()
    //   {
    //       playerOne = GameObject.FindGameObjectWithTag("Player");
    //       playerTwo = GameObject.FindGameObjectWithTag("Player2");
    //}

    //   // Update is called once per frame
    //   void Update ()
    //   {
    //       /*Dont move camera if players are a certain distance apart, still need to stop players from 
    //       //being able to move outside camera

    //       //playerDistance = (playerOne.transform.position - playerTwo.transform.position).magnitude;
    //       if (playerDistance < 15)*/
    //           //Call FixedCameraFollowSmooth

    //       FixedCameraFollowSmooth(gameObject.GetComponent<Camera>(), playerOne.transform, playerTwo.transform);
    //   }


    //   public void FixedCameraFollowSmooth(Camera cam, Transform t1, Transform t2)
    //   {
    //       // How many units should we keep from the players
    //       float followTimeDelta = 0.8f;

    //       // Midpoint we're after
    //       Vector3 midpoint = (t1.position + t2.position) / 2f;

    //       // Distance between objects
    //       float distance = (t1.position - t2.position).magnitude;

    //       // Move camera a certain distance
    //       Vector3 cameraDestination = midpoint - cam.transform.forward;

    //       // Adjust ortho size if we're using one of those
    //       if (cam.orthographic)
    //       {
    //           //Use this if you want the camera to zoom based on distance between players

    //           //if (distance < 5)
    //           //    cam.orthographicSize = 5;
    //           //else if (distance >= 5 && distance < 10)
    //           //    cam.orthographicSize = distance;
    //           //else if (distance > 10)
    //           //    cam.orthographicSize = 10;
    //       }

    //       cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

    //       // Snap when close enough to prevent annoying slerp behavior
    //       if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
    //           cam.transform.position = cameraDestination;

    //       //Debug.Log(cam.transform.position);
    //   }
}
