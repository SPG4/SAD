using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public GameObject diaBox;
    public Text diaText;
    public bool diaActive;
    public string[] diaLines;
    public int currentLine;

    void Update()
    {

        if (diaActive && Input.GetKeyDown(KeyCode.Space))
        {
            //diaBox.SetActive(false);
            //diaActive = false;
            currentLine++;
        }

        if (currentLine >= diaLines.Length)
        {
            diaBox.SetActive(false);
            diaActive = false;
            currentLine = 0;
        }
        diaText.text = diaLines[currentLine];
    }

    public void ShowBox(string dialogue)
    {
        diaActive = true;
        diaBox.SetActive(true);
        diaText.text = dialogue;
    }

    public void ShowDialogue()
    {
        diaActive = true;
        diaBox.SetActive(true);
    }
}