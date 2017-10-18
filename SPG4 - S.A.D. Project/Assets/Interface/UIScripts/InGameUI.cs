using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {

    private string currentP1Ability;
    private string currentP2Ability;
    public Text p1MainAbilityText;
    public PlayerAbilities playerOne;
    public PlayerAbilities playerTwo;
    int playerNumber;
	// Use this for initialization
	void Start () {
        playerNumber = 1;
        GetChosenAbility();

    }
	
	// Update is called once per frame
	void Update () {
        GetChosenAbility();
	}

    void GetChosenAbility()
    {
        //    string s = player.gameObject.CompareTag("Current Ability");
        //    currentAbility = s;
        currentP1Ability = playerOne.GetCurrentAbility();
    }
}
