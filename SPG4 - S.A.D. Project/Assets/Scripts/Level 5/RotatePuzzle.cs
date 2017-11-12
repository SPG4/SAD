using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePuzzle : MonoBehaviour {

    public float rotationSpeed;
    public GameObject rotateObject;
    public bool reverseRotation = false;

    private int reverser = 1;
    void Start()
    {
        if(reverseRotation == true)
        {
            reverser *= -1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (true)
        {
            rotateObject.transform.Rotate(Vector3.forward * (((Time.deltaTime * 10) * rotationSpeed))* reverser );
        }
    }
}
