using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {

    private string currentP1Ability;
    private string currentP2Ability;
    public Text p2MainAbilityText;
    public Text p1MainAbilityText;
    public PlayerAbilities playerOne;
    public PlayerAbilities playerTwo;
	// Use this for initialization
	void Start () {
        GetChosenAbility();

    }
	
	// Update is called once per frame
	void Update () {
        GetChosenAbility();
        p2MainAbilityText.text = currentP1Ability;
        p1MainAbilityText.text = currentP2Ability;
	}

    void GetChosenAbility()
    {
        //    string s = player.gameObject.CompareTag("Current Ability");
        //    currentAbility = s;
        currentP2Ability = playerOne.GetCurrentAbility();
        currentP1Ability = playerTwo.GetCurrentAbility();
    }
}
