using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHatch : MonoBehaviour {

    public GameObject hatch;
    bool one, two, three;

	void Start () {
		
	}
	

	void Update ()
    {
        if (one && two && three)
            hatch.GetComponent<DistanceJoint2D>().breakForce = 1;
    }

    void CheckColor(int nr)
    {
       if (transform.GetChild(nr).gameObject.GetComponent<SpriteRenderer>().color == Color.green)
        {
            if (nr == 1)
                one = true;
            if (nr == 2)
                two = true;
            if (nr == 3)
                three = true;
        }
       else
        {
            if (nr == 1)
                one = false;
            if (nr == 2)
                two = false;
            if (nr == 3)
                three = false;
        }
    }
}
