using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour {

    public int piece;
	void Start ()
    {
        if (piece == 1)
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        if (piece == 2)
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

        if (piece == 3)
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
	

	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PuzzlePiece")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }

}
