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
    private Vector2 zeroPosTurret;
    private float target1Sum;
    private float target2Sum;

    private void Start()
    {
        zeroPosTurret = transform.position - transform.position;
        //player1 = GameObject.Find("Player 1");
        target1Sum = Mathf.Infinity;
    }

    /// <summary>
    /// Checks the closest target and rotates towards that target.
    /// </summary>
    public void Update()
    {
        target1Sum = 0;
        target2Sum = 0;

        Vector2 direction1 = target1.position - transform.position;
        if(direction1.x < 0)
        {
           target1Sum += direction1.x * -1;
        }
        else { target1Sum += direction1.x; }
        if (direction1.y < 0)
        {
            target1Sum += direction1.y * -1;
        }
        else { target1Sum += direction1.y; }


        Vector2 direction2 = target2.position - transform.position;
        if (direction2.x < 0)
        {
            target2Sum += direction2.x * -1;
        }
        else { target2Sum += direction2.x; }

        if (direction2.y < 0)
        {
           target2Sum += direction2.y * -1;
        }
        else { target2Sum += direction2.y; }


        //target1Sum = direction1.x + direction1.y;
        //target2Sum = direction2.x + direction2.y;

        //if(direction1.x < direction2.x && direction1.y < direction2.y)
        if(target1Sum < target2Sum)
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