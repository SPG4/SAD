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
    public List<string> diaList = new List<string>();
    public bool useList;
    bool player1Input = false;
    bool player2Input = false;

    void Update()
    {
        if (Input.GetButtonDown("Next1"))
            player1Input = true;

        if (Input.GetButtonDown("Next2"))
            player2Input = true;

        if (diaActive && player1Input && player2Input)
        {
            //diaBox.SetActive(false);
            //diaActive = false;
            player1Input = false;
            player2Input = false;
            currentLine++;
        }
        if (useList)
        {
            if (currentLine >= diaList.Count)
            {
                diaBox.SetActive(false);
                diaActive = false;
            }
            else
                diaText.text = diaList[currentLine];
        }
        else if (currentLine >= diaLines.Length)
        {
            diaBox.SetActive(false);
            diaActive = false;
            currentLine = 0;
        }
        if (!useList)
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

    public void AddText(string text)
    {
        diaList.Add(text);
        ShowDialogue();
    }
}