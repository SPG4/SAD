using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLevelCamera : MonoBehaviour {

    public float speed;
    bool wakeup = true;
    Vector3 rotateDirection;
    Vector3 toPosition;
    public GameObject point1;
    public GameObject point2;

	void Start ()
    {
        speed = 2;
        gameObject.transform.position = new Vector3(5.3f, 2.6f, 4.4f);
        gameObject.transform.rotation = Quaternion.Euler(-77, -428, -16);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (wakeup)
        {
            toPosition = new Vector3 (point1.transform.position.x +0.1f, point1.transform.position.y + 0.1f, point1.transform.position.z + 0.1f);
            //toPosition = new Vector3(4.5f, 3, 4.5f);
            //rotateDirection = new Vector3(-1.7f, -443.5f, -3.5f);

            var rotate = Quaternion.LookRotation(point1.transform.position - transform.position);

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, toPosition, step);

            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rotate, step *5);

            if (transform.position == toPosition && Mathf.Abs(gameObject.transform.rotation.x) == Mathf.Abs(Quaternion.LookRotation(point1.transform.position - transform.position).x))
            {
                wakeup = false;
                print("false");
            }
        }


        if (Input.GetButton("Vertical1") && Input.GetButton("Vertical2") && !wakeup)
        {
            Debug.Log(gameObject.transform.rotation);
            Debug.Log(Quaternion.LookRotation(point1.transform.position - transform.position));
            toPosition = new Vector3(4.25f, 4.8f, 1.8f);
            var rotate = Quaternion.LookRotation(point2.transform.position - transform.position);

            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rotate, step * 20);

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, toPosition, step);
            
        }
            
    }
}
