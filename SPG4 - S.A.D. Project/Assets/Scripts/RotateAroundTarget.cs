using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundTarget : MonoBehaviour {

    public Transform target;
    public float angleMax = 90f;

    private Vector3 initialVector = Vector3.right;
    
    //Old field
    private Vector3 zAxis = new Vector3(0, 0, 1);

    private void Start()
    {
        if (target != null)
        {
            initialVector = transform.position - target.position;
        }
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
            
    }

    void RotateCrosshair(float rotateDegrees)
    {
        if (target != null)
        {
            Vector3 currentVector = transform.position - target.position;

            float angleBetween = Vector3.Angle(initialVector, currentVector) * (Vector3.Cross(initialVector, currentVector).z > 0 ? 1 : -1);
            float newAngle = Mathf.Clamp(angleBetween + rotateDegrees, -angleMax, angleMax);
            rotateDegrees = newAngle - angleBetween;

            transform.RotateAround(target.position, Vector3.forward, rotateDegrees);
        }
    }

    void SetInitialVector(Vector3 playerFacingVector)
    {
        initialVector = playerFacingVector;
    }

    //Old method
    void RotateObject(float rotSpeed)
    {
        transform.RotateAround(target.position, zAxis, rotSpeed);
    }
}
