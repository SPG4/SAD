using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public int useAbilityP1 = -1, useAbilityP2 = -1;
    public GameObject newAbilityObject;
    public string dialogue;
    private DialogueManager diaManager;
    public string[] diaLines;
    public bool useSpecial;
    bool doneOnce = false;


    bool active = false;

    void Start()
    {
        diaManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if (active)
        {
            if (diaManager.currentLine == useAbilityP1)
            {
                diaManager.mayContinue = false;
                if (newAbilityObject != null && newAbilityObject.GetComponent<InteractableObjects>().usedP1 == true)
                {
                    diaManager.currentLine++;
                    //newAbilityObject.GetComponent<InteractableObjects>().usedP1 = false;
                }
            }

            else if (diaManager.currentLine == useAbilityP2)
            {
                diaManager.mayContinue = false;
                if (newAbilityObject != null && newAbilityObject.GetComponent<InteractableObjects>().usedP2 == true)
                {
                    diaManager.currentLine++;
                    //newAbilityObject.GetComponent<InteractableObjects>().usedP2 = false;
                }
            }
            else
                diaManager.mayContinue = true; 
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        //if (other.gameObject.name == "Player")
        {
            //diaManager.ShowBox(dialogue);
            if (useSpecial)
            {
                if (!doneOnce)
                {
                    for (int i = 0; i < diaLines.Length; i++)
                    {
                        diaManager.SendMessage("AddText", diaLines[i]);
                    }
                    doneOnce = true;
                    active = true;
                }
            }
            else if (!diaManager.diaActive)
            {               
                diaManager.diaLines = diaLines;
                diaManager.currentLine = 0;
                diaManager.ShowDialogue();
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}