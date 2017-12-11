using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {

    private string currentP1Ability;
    private string currentP2Ability;
    public PlayerAbilities playerOne;
    public PlayerAbilities playerTwo;

    public GameObject p1Standard;
    public GameObject p1TP;
    public GameObject p1Size;
    public GameObject p1Shield;
    public GameObject p2Standard;
    public GameObject p2TP;
    public GameObject p2Size;
    public GameObject p2Shield;

    // Use this for initialization
    void Start () {
        GetChosenAbility();
    }
	
	// Update is called once per frame
	void Update () {
        GetChosenAbility();

        if(currentP2Ability == "ShootTeleportBall")
        {
            p1TP.gameObject.SetActive(true);
            p1Standard.gameObject.SetActive(false);
            p1Size.gameObject.SetActive(false);
            p1Shield.gameObject.SetActive(false);
        }
        if (currentP2Ability == "Shield")
        {
            p1TP.gameObject.SetActive(false);
            p1Standard.gameObject.SetActive(false);
            p1Size.gameObject.SetActive(false);
            p1Shield.gameObject.SetActive(true);
        }
        if (currentP2Ability == "StandardAbility")
        {
            p1TP.gameObject.SetActive(false);
            p1Standard.gameObject.SetActive(true);
            p1Size.gameObject.SetActive(false);
            p1Shield.gameObject.SetActive(false);
        }
        if (currentP2Ability == "SizeGun")
        {
            p1TP.gameObject.SetActive(false);
            p1Standard.gameObject.SetActive(false);
            p1Size.gameObject.SetActive(true);
            p1Shield.gameObject.SetActive(false);
        }
        if (currentP1Ability == "ShootTeleportBall")
        {
            p2TP.gameObject.SetActive(true);
            p2Standard.gameObject.SetActive(false);
            p2Size.gameObject.SetActive(false);
            p2Shield.gameObject.SetActive(false);
        }
        if (currentP1Ability == "Shield")
        {
            p2TP.gameObject.SetActive(false);
            p2Standard.gameObject.SetActive(false);
            p2Size.gameObject.SetActive(false);
            p2Shield.gameObject.SetActive(true);
        }
        if (currentP1Ability == "StandardAbility")
        {
            p2TP.gameObject.SetActive(false);
            p2Standard.gameObject.SetActive(true);
            p2Size.gameObject.SetActive(false);
            p2Shield.gameObject.SetActive(false);
        }
        if (currentP1Ability == "SizeGun")
        {
            p2TP.gameObject.SetActive(false);
            p2Standard.gameObject.SetActive(false);
            p2Size.gameObject.SetActive(true);
            p2Shield.gameObject.SetActive(false);
        }
    }

    void GetChosenAbility()
    {
        currentP2Ability = playerOne.GetCurrentAbility();
        currentP1Ability = playerTwo.GetCurrentAbility();
    }
}
