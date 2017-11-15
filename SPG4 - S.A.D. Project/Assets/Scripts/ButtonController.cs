using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    private bool activated;
    private GameObject eyeSprite;
    private GameObject eyeBall;
    private GameObject objectToMove;

    private float timer = 0;

	// Use this for initialization
	void Start ()
    {
        eyeSprite = this.gameObject.transform.Find("eyesocket_eye").gameObject;
        eyeBall = this.gameObject.transform.Find("eyeball").gameObject;
        objectToMove = this.gameObject.transform.Find("Wall1 (5)").gameObject;
        Debug.Log(objectToMove);
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == eyeBall.gameObject)
        {
            Destroy(eyeBall);
            eyeSprite.SetActive(true);
            activated = true;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (activated)
            timer += Time.deltaTime;

        if (activated && timer < 5)
        {
            Vector2 objectToMoveVelocity = new Vector2(0, 1);
            gameObject.GetComponent<Rigidbody2D>().velocity = objectToMoveVelocity;
        }
        else
        {
            activated = false;
            timer = 0;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
