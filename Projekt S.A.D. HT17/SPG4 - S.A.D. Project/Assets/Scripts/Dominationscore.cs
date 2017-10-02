using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dominationscore : MonoBehaviour {

    int scoreP1;
    int scoreP2;

	void Start () 
    {
        scoreP1 = 0;
        scoreP2 = 0;
	}
	
    void Player1()
    {
        scoreP1++;
    }

    void Player2()
    {
        scoreP2++;
    }

    void EndGame()
    {
       //algoritm för de olika sluten
    }

}
