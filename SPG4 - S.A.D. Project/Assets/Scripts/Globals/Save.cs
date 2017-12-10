using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour {

    public static Save Instance { get; private set; }
    int totalAdded;
    int sessionAddedP1, sessionAddedP2;
    string currentpoints;
    public GameObject player1, player2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //Not necessary to have players atm. Can be removed
        if(player1 == null)
        {
            player1 = GameObject.FindGameObjectWithTag("Player");
        }
        if(player2 == null)
        {
            player2 = GameObject.FindGameObjectWithTag("Player2");
        }

        //Get the last score from save file. Not necessary
        totalAdded = PlayerPrefs.GetInt("Player1Score");
        totalAdded = PlayerPrefs.GetInt("Player2Score");
    }

    private void Update()
    {

        //Below is for TESTING PURPOSE
        totalAdded = PlayerPrefs.GetInt("Player2Score");
        currentpoints = " " + totalAdded;
        if (Input.GetKeyDown("return"))
        {
            SavePlayerUse(sessionAddedP1, sessionAddedP2);
        }
    }

    /// <summary>
    /// Save the amount of points from the "session" (usually at the end of a level)
    /// </summary>
    public void SavePlayerUse(int addedP1, int addedP2)
    {
        PlayerPrefs.SetInt("Player1Score", GetTotalPlayerPoint(1) + addedP1);
        PlayerPrefs.SetInt("Player2Score", GetTotalPlayerPoint(2) + addedP2);
        this.sessionAddedP1 = 0; //reset the session count
        this.sessionAddedP2 = 0;
    }


    /// <summary>
    /// Add an amount of points used for correct player
    /// </summary>
    /// <param name="totalAdded"></param>
    /// <param name="playerNr"></param>
    public void AddOneUseOfStandard(int playerNr)
    {
        if (playerNr == 1)
            this.sessionAddedP1++;
        else if (playerNr == 2)
            this.sessionAddedP2++;
    }
    

    /// <summary>
    /// Get the total amount of times total a player has used their standard ability
    /// </summary>
    /// <param name="playerNr">The number of the player</param>
    /// <returns></returns>
    public int GetTotalPlayerPoint(int playerNr)
    {
        return PlayerPrefs.GetInt("Player"+playerNr+"Score");
    }
}
