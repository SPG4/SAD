using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundTarget : MonoBehaviour {

    public float speed;
    public float originalSpeed;
    public Transform target;
    //PlayerController player;

    private Vector3 zAxis = new Vector3(0, 0, 1);

    private void Start()
    {
        //player.gameObject.GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
            
    }

    void RotateObject(float rotSpeed)
    {
        transform.RotateAround(target.position, zAxis, rotSpeed);
    }
}
