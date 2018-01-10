using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public int useAbilityP1 = -1, useAbilityP2 = -1;
    public GameObject newAbilityObject;
    public string dialogue;
    private DialogueManager diaManager;
    private PlayerAbilities P1, P2;
    public string[] diaLines;
    public bool useSpecial;
    bool doneOnce = false;
    bool setfalse1, setfalse2;
    public int movePos = 0;
    GameObject camera;
    bool changeBackCamera = true;


    void Start()
    {
        diaManager = FindObjectOfType<DialogueManager>();

        P1 = GameObject.Find("Player 1").GetComponent<PlayerAbilities>();
        P2 = GameObject.Find("Player 2").GetComponent<PlayerAbilities>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        if (diaManager.diaActive)
        {
            P2.canUseability = false;
            P1.canUseability = false;
            if (diaManager.currentLine == useAbilityP1)
            {
                P1.canUseability = true;
                if (!setfalse1)
                {
                    //newAbilityObject.GetComponent<InteractableObjects>().usedP1 = false;
                    setfalse1 = true;
                }

                diaManager.mayContinue = false;
                if (newAbilityObject != null && newAbilityObject.GetComponent<InteractableObjects>().usedP1 == true || P1.usedP1 == true)
                {
                    diaManager.currentLine++;
                }
            }
            else
            {
                P2.canUseability = true;
                P1.canUseability = true;
            }

            if (diaManager.currentLine == useAbilityP2)
            {
                P2.canUseability = true;
                if (!setfalse2)
                {
                    //newAbilityObject.GetComponent<InteractableObjects>().usedP2 = false;
                    setfalse2 = true;
                }
                diaManager.mayContinue = false;
                if (newAbilityObject != null && newAbilityObject.GetComponent<InteractableObjects>().usedP2 == true || P2.usedP2 == true)
                {
                    diaManager.currentLine++;              
                }
            }
            else
                diaManager.mayContinue = true; 
        }

        if (doneOnce && !diaManager.diaActive && changeBackCamera)
        {
            camera.GetComponent<CameraController>().minSizeY += 2;
            camera.GetComponent<CameraController>().camYOffset += 2;
            changeBackCamera = false;
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
                    if (other.gameObject.tag == "Player")
                    {
                        P1.gameObject.GetComponent<Transform>().position += new Vector3(movePos, 0, 0);
                        P2.gameObject.GetComponent<Transform>().position = P1.gameObject.GetComponent<Transform>().position - new Vector3(movePos, 0, 0);
                        camera.GetComponent<CameraController>().minSizeY -= 2;
                        camera.GetComponent<CameraController>().camYOffset -= 2;
                    }
                    else
                    {
                        P2.gameObject.GetComponent<Transform>().position += new Vector3(movePos, 0, 0);
                        P1.gameObject.GetComponent<Transform>().position = P2.gameObject.GetComponent<Transform>().position - new Vector3(movePos, 0, 0);
                        camera.GetComponent<CameraController>().minSizeY -= 2;
                        camera.GetComponent<CameraController>().camYOffset -= 2;
                    }
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