using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectingShield : MonoBehaviour {

    /// <summary>
    /// Checks collision and if the object should "bounce" off the shield it will be given a new velocity
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 13)
        {
            Vector3 newVelocity;
            if (this.transform.parent.gameObject.transform.localScale.x == 1 && other.gameObject.GetComponent<Rigidbody2D>().velocity.x < 0 ||
                this.transform.parent.gameObject.transform.localScale.x == -1 && other.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                newVelocity = Vector3.Reflect(other.gameObject.GetComponent<Rigidbody2D>().velocity / 5, Vector3.right); //fix variable for speed? is set in "Turret" as "speedFactor"
                other.gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity * 5;
            }
        }
    }
}

