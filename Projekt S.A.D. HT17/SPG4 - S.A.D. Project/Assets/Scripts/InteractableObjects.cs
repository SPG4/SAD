using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour {

    float force = 400;
    // Vector2 direction = new Vector2(1, 0);

    void Start()
    {

    }

    void Update()
    {


    }

    void StandardAbility(Vector2 direction)
    {
        Debug.Log("Using Ability");
        GetComponent<Rigidbody2D>().AddRelativeForce(direction * force);
    }

    void SizeGun(object stuff)
    {
        transform.localScale += new Vector3(1, 1, 0);
    }


}
