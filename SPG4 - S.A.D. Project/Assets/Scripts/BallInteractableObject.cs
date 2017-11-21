using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInteractableObject : MonoBehaviour {

    float force = 40;

    /// <summary>
    /// Method called when using standard ability, moves affected object
    /// </summary>
    /// <param name="direction"></param>
    void StandardAbility(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * force);
    }

    /// <summary>
    /// Method called when using the size gun ability
    /// </summary>
    /// <param name="player">The player using the ability</param>
    void SizeGun(string player)
    {
        //Ball not affected by SizeGun
    }
}
