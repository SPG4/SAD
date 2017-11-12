using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    float force = 400;
    // Vector2 direction = new Vector2(1, 0);

    Vector3 maxLocalScale, scale;
    float maxlocalScaleMagnitude;

    void Start()
    {
        maxLocalScale = new Vector3(1.5f, 1.5f, 0); //Maximum/minimum-scale
        maxlocalScaleMagnitude = maxLocalScale.magnitude;
        scale = new Vector3(0.1f, 0.1f, 0);
    }

    void Update()
    {

    }
    /// <summary>
    /// Method called when using standard ability, moves affected object
    /// </summary>
    /// <param name="direction"></param>
    void StandardAbility(Vector2 direction)
    {
       // Debug.Log("Using Ability");
        GetComponent<Rigidbody2D>().AddForce(direction * force);
    }

    /// <summary>
    /// Method called when using the size gun ability
    /// </summary>
    /// <param name="player">The player using the ability</param>
    void SizeGun(string player)
    {
        float newLocalScaleMagnitude = transform.localScale.magnitude;

        if (newLocalScaleMagnitude < maxlocalScaleMagnitude) //The object can be scaled as long it hasn't reached its max/min-scale
            if (player == "UseAbilityP1")
                transform.localScale += scale; //Scale up

        if (player == "UseAbilityP2")
            transform.localScale += -scale; //Scale down
    }
}
