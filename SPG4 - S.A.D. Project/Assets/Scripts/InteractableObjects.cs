using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    float force = 100;
    // Vector2 direction = new Vector2(1, 0);

    Vector3 maxLocalScale, scale;
    float maxlocalScaleMagnitude;
    public bool usedP1 = false, usedP2 = false;

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
        if (this.gameObject.tag == "Ball1" || this.gameObject.tag == "Ball2" || this.gameObject.name == "eyeball")
            GetComponent<Rigidbody2D>().AddForce(direction * force * 35);
        else
        {
            float mass = GetComponent<Rigidbody2D>().mass;
            GetComponent<Rigidbody2D>().AddForce(direction * mass * force);
        }
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
            {
                transform.localScale += scale; //Scale up
                usedP1 = true;
            }

        if (player == "UseAbilityP2")
        {
            usedP2 = true;
            transform.localScale += -scale; //Scale down
        }
    }
}
