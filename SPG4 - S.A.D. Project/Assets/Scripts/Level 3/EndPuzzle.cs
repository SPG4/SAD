using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPuzzle : MonoBehaviour {

    Vector3 maxLocalScale, scale;
    float maxlocalScaleMagnitude;
    Vector3 startPos;
    GameObject weightCheck;
    public bool isStone;
    float force = 400;


    void Start()
    {
        maxLocalScale = new Vector3(1.5f, 1.5f, 0); //Maximum/minimum-scale
        maxlocalScaleMagnitude = maxLocalScale.magnitude;
        scale = new Vector3(0.1f, 0.1f, 0);
        startPos = this.transform.position;
        weightCheck = this.transform.root.gameObject;
    }

    void SizeGun(string player)
    {
        float newLocalScaleMagnitude = transform.localScale.magnitude;

        if (newLocalScaleMagnitude < maxlocalScaleMagnitude) //The object can be scaled as long it hasn't reached its max/min-scale
            if (player == "UseAbilityP1")
            {
                transform.localScale += scale; //Scale up
                GetComponent<Rigidbody2D>().mass += 10;
            }

        if (player == "UseAbilityP2")
        {
            transform.localScale += -scale; //Scale down
            GetComponent<Rigidbody2D>().mass -= 10;
        }
    }

    void StandardAbility(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * force);
    }

    /// <summary>
    /// Weights script will Send Message here to change objects position
    /// </summary>
    /// <param name="pos"></param>
    void ChangePos(Vector3 pos)
    {
        this.transform.position = pos;
    }

}
