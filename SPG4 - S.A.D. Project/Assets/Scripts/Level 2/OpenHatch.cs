using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHatch : MonoBehaviour {

    public GameObject hatch;
    public bool one, two, three;

	void Start () {
		
	}
	

	void Update ()
    {
        if (one && two && three)
        {
            print("Success");
            hatch.GetComponent<DistanceJoint2D>().breakForce = 1;
        }
    }

    void CheckColor(int nr)
    {
       if (transform.GetChild(nr).gameObject.GetComponent<SpriteRenderer>().color == Color.green)
        {
            if (nr == 0)
                one = true;
            if (nr == 1)
                two = true;
            if (nr == 2)
                three = true;
        }
       else
        {
            if (nr == 0)
                one = false;
            if (nr == 1)
                two = false;
            if (nr == 2)
                three = false;
        }
    }
}
