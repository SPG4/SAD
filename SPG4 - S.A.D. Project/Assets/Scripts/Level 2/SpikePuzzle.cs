using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePuzzle : MonoBehaviour
{
    bool hasMoved = false;
    float angle = 5;

    void Start ()
    {
		
	}
	
	void Update () {
		
	}

    void StandardAbility(Vector2 direction)
    {
        if (!hasMoved)
        {
            if (direction.x < 0)
            {
                gameObject.GetComponent<Rigidbody2D>().MoveRotation(angle);
                gameObject.GetComponent<Transform>().position += new Vector3(-0.25f, 0, 0);
                hasMoved = true;
            }
        }
    }

}
