using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weights : MonoBehaviour {

    public float stoneMass, boxMass;
    public GameObject box, stone;
    public float speed;

    void Start()
    {

    }
    /// <summary>
    /// Changes the box/stone's position based on thier mass
    /// </summary>
    void Update()
    {
        float step = speed * Time.deltaTime;
        stoneMass = stone.GetComponent<Rigidbody2D>().mass;
        boxMass = box.GetComponent<Rigidbody2D>().mass;

        if (stoneMass == boxMass)
        {
            //print("=");
            if (stone.transform.position != new Vector3(124.6f, -10, 0))
            {
                stone.transform.position = Vector3.MoveTowards(stone.transform.position, new Vector3(124.6f, -10, 0), step);
            }

            if(box.transform.position != new Vector3(132.05f, -6, 0))
            {
                box.transform.position = Vector3.MoveTowards(box.transform.position, new Vector3(132.05f, -6, 0), step);
            }
        }

        if (stoneMass < boxMass)
        {
            //print("big box");
            if (stone.transform.position != new Vector3(124.6f, -4, 0))
            {
                stone.transform.position = Vector3.MoveTowards(stone.transform.position, new Vector3(124.6f, -4, 0), step);
            }

            if(box.transform.position != new Vector3(132.05f, -12, 0))
            {
                box.transform.position = Vector3.MoveTowards(box.transform.position, new Vector3(132.05f, -12, 0), step);
            }
        }

        if (stoneMass > boxMass)
        {
            //print("big stone");
            
            if (stone.transform.position != new Vector3(124.6f, -10, 0))
            {                
                stone.transform.position = Vector3.MoveTowards(stone.transform.position, new Vector3(124.6f, -10, 0), step);
            }
            
            if(box.transform.position != new Vector3(132.05f, -6, 0))
            {
                box.transform.position = Vector3.MoveTowards(box.transform.position, new Vector3(132.05f, -6, 0), step);
            }
        }

    }
}
