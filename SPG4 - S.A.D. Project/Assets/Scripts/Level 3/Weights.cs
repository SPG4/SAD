using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weights : MonoBehaviour {

    public float stoneMass, boxMass;
    public GameObject box, stone;

    void Start()
    {

    }
    /// <summary>
    /// Changes the box/stone's position based on thier mass
    /// </summary>
    void Update()
    {
        stoneMass = stone.GetComponent<Rigidbody2D>().mass;
        boxMass = box.GetComponent<Rigidbody2D>().mass;

        if (stoneMass == boxMass)
        {
            //print("=");
            stone.transform.position = new Vector3(110, -10, 0);
            box.transform.position = new Vector3(117.4f, -9, 0);
        }

        if (stoneMass < boxMass)
        {
            //print("big box");
            stone.transform.position = new Vector3(110, 0, 0);
            box.transform.position = new Vector3(117.4f, -12, 0);
        }

        if (stoneMass > boxMass)
        {
            //print("big stone");
            stone.transform.position = new Vector3(110, -10, 0);
            box.transform.position = new Vector3(117.4f, -9, 0);
        }

    }

}
