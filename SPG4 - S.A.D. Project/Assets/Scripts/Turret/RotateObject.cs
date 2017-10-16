// Maximilian Törn Almö
// 10-14

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed = 5f;
    public Transform target1;
    public Transform target2;


    private Vector2 closestTarget;

    private void Start()
    {
        //player1 = GameObject.Find("Player 1");
        
    }

    /// <summary>
    /// Checks the closest target and rotates towards that target.
    /// </summary>
    public void Update()
    {
        Vector2 direction2 = target2.position - transform.position;
        Vector2 direction1 = target1.position - transform.position;
        if(direction1.x < direction2.x && direction1.y < direction2.y)
        {
            closestTarget = direction1;
        }
        else
        {
            closestTarget = direction2;
        }
        float angle = Mathf.Atan2(closestTarget.y, closestTarget.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}   