using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLevel3 : MonoBehaviour
{
    GameObject dialogue;
    public int index;

    void Start()
    {
        dialogue = GameObject.FindGameObjectWithTag("DialogueManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (index == 1)
            {
                dialogue.SendMessage("AddText", "Green: This music is too loud!");
                dialogue.SendMessage("AddText", "Blue: You call this noise music?");
                dialogue.SendMessage("AddText", "Whatever, we have to stop it somehow.");
            }

            if (index == 2)
            {
                dialogue.SendMessage("AddText", "Phew, we did it!");
            }

            if (index == 3)
            {
                dialogue.SendMessage("AddText", "Green: Oh, no! I fell down!");
                dialogue.SendMessage("AddText", "Blue: God, you're so clumsy!");
            }

            if (index == 4)
            {
                dialogue.SendMessage("AddText", "Green: Watch out for that rat trap down there!");
            }

            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
