using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePuzzle : MonoBehaviour
{

    float angle = 5;

    void Start ()
    {
		
	}
	
	void Update () {
		
	}

    void StandardAbility(Vector2 direction)
    {
        Debug.Log("Using Ability");
        
        gameObject.GetComponent<Rigidbody2D>().MoveRotation(angle);
    }

}
