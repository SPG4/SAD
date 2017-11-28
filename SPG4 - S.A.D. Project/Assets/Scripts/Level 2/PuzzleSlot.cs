using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlot : MonoBehaviour {

    public int slot;

	void Start ()
    {

	}
	
	void Update ()
    {

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PuzzlePiece")
        {
            collision.gameObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;

            if (slot == 1)
            {
                if (collision.gameObject.GetComponent<SpriteRenderer>().color == Color.blue)
                    gameObject.transform.root.SendMessage("PieceCorrect", slot);
                else
                    gameObject.transform.root.SendMessage("PieceIncorrect", slot);
            }
            if (slot == 2)
            {
                if (collision.gameObject.GetComponent<SpriteRenderer>().color == Color.red)
                    gameObject.transform.root.SendMessage("PieceCorrect", slot);
                else
                    gameObject.transform.root.SendMessage("PieceIncorrect", slot);
            }

            if (slot == 3)
            {
                if (collision.gameObject.GetComponent<SpriteRenderer>().color == Color.green)
                    gameObject.transform.root.SendMessage("PieceCorrect", slot);
                else
                    gameObject.transform.root.SendMessage("PieceIncorrect", slot);
            }

        }
    }
}
