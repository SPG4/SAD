using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour {

    public string dialogue;
    private DialogueManager diaManager;

    // Use this for initialization
    void Start () {
        diaManager = FindObjectOfType<DialogueManager>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
                diaManager.ShowBox(dialogue);            
        }
    }
}
