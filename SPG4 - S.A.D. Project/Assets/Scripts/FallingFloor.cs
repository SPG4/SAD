// Maximilian Törn Almö
// 2017-10-08

//      *** Description ***
//  Makes so objects starts to fall down one at a time from starting with index 0.
//  Adds +1 to index after each iteration.
//  
//  Add script to object PARENT, script effects children.
//  Make sure the children got the component "Rigidbody 2D" and
//  that body type is NOT set to DYNAMIC.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour
{
    [Tooltip("Determines how many seconds the script will wait before starting")]
    public float waitToStart = 0.0f;
    [Tooltip("How often each object should fall.")]
    public float dropEverySeconds = 0.0f;

    private GameObject[] children;
    private float origEverySeconds = 0.0f;
    private int i = 0;
    private int numberOfChildren = 0;

    private bool switchChild = false;
    private bool canStart = false;

    // Use this for initialization
    void Start()
    {
        origEverySeconds = dropEverySeconds;
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
            ++numberOfChildren;
        }
    }

    void FixedUpdate()
    {
        if (canStart == true)
        {
            if (switchChild == false && i < numberOfChildren)
            {
                Rigidbody2D rb = children[i].GetComponent<Rigidbody2D>();

                if (0 < dropEverySeconds)
                {
                    dropEverySeconds -= Time.fixedDeltaTime;
                }
                else if (rb.bodyType != RigidbodyType2D.Dynamic && switchChild == false)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    switchChild = true;
                }

            }
            else if (switchChild == true && i < numberOfChildren)
            {
                if (dropEverySeconds <= 0)
                {
                    dropEverySeconds = origEverySeconds;
                }
                ++i;
                switchChild = false;
            }
        }
        else
        {
            waitToStart -= Time.fixedDeltaTime;
            if(waitToStart <= 0)
            {
                canStart = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
