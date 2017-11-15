using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{

    public string dialogue;
    private DialogueManager diaManager;
    public string[] diaLines;

    void Start()
    {
        diaManager = FindObjectOfType<DialogueManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        //if (other.gameObject.name == "Player")
        {
            //diaManager.ShowBox(dialogue);

            if (!diaManager.diaActive)
            {
                diaManager.diaLines = diaLines;
                diaManager.currentLine = 0;
                diaManager.ShowDialogue();
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}