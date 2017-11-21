using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyButtonController : MonoBehaviour {

    Transform parent;
    Vector2 direction;
    public bool hasBeenTriggered;
    float speed = 1;

	// Use this for initialization
	void Start () {
        parent = gameObject.transform.parent;
        direction = parent.transform.position - this.gameObject.transform.position;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Black hole exit")
            GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
		if (hasBeenTriggered)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
	}
}
