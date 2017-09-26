using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController playerOne;
    public PlayerController playerTwo;

    public Vector2 playerDistance;
    private Vector2 cameraPosition;
    private Vector2 cameraOffset;
    private Vector3 cameraV2toV3;
    private Vector3 velocity;

    public float distance;
    private float smoothTime;

    // Use this for initialization
    void Start ()
    {

	}

    private void Awake()
    {
        smoothTime = 0.2f;
    }

    // Update is called once per frame
    void Update ()
    {
        PlayerDistance();
        CenterPosition();
        UpdateCamera();
	}

    private void PlayerDistance()
    {

        //This will have to depend on which of the players that is closest to the next goal
        playerDistance = (playerOne.position - playerTwo.position);
        //playerDistance = (playerTwo.position - playerOne.position);

        distance = Mathf.Sqrt((playerDistance.x * playerDistance.x) + (playerDistance.y * playerDistance.y));
    }

    private void CenterPosition()
    {
        //cameraPosition = playerDistance / 2;
        cameraPosition.x = distance / 2;
    }

    private void UpdateCamera()
    {     
        cameraV2toV3.x = cameraPosition.x;
        cameraV2toV3.y = cameraPosition.y;
        cameraV2toV3.z = -10f;

        transform.position = Vector3.SmoothDamp(transform.position, cameraV2toV3, ref velocity, smoothTime);
    }
}
