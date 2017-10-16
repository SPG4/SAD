using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityArea : MonoBehaviour {

    bool player1Ready, player2Ready, playersReady;
    string currentAbility;
    GameObject abilityArea;

	// Use this for initialization
	void Start () {
        CheckLatestUsedAbility();
    }
	
	// Update is called once per frame
	void Update () {

        
        if (!playersReady)
        {
            CheckPlayerInput(1);
            CheckPlayerInput(2);
        }
        else
        {
            Destroy(this);
        }

    }

    void CheckPlayerInput(int p)
    {
        if (Input.GetAxis("Vertical"+p) == 1)
        {
            //Call animation "down"
            //Set current ability
        }
        else if (Input.GetAxis("Vertical"+p) == -1)
        {
            //Call animation "up"
            //Set current ability
        }
        else if (Input.GetAxis("Submit") == 1)
        {
            //Remove boxes (add green checkbox?)
            //Set current ability
            if (p == 1)
            {
                gameObject.SendMessage(currentAbility);
                player1Ready = true;
            }
            else
                player2Ready = true;
        }
        
    }

    void CheckLatestUsedAbility()
    {
        //TODO set current ability to last used (before death for example)
    }

    void LoadAbilities()
    {
        //TODO load all existing abilities?????????????
    }

    void ReadyCheck()
    {
        if (player1Ready && player2Ready)
        {
            playersReady = true;
        }
    }
}
