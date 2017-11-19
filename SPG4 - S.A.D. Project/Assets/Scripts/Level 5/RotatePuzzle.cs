using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for Puzzle 4 on Level 5.
/// Rotates target object while player is within trigger.
/// </summary>
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            rotateObject.transform.Rotate(Vector3.forward * (((Time.deltaTime * 10) * rotationSpeed)) * reverser);
        }
    }
}
