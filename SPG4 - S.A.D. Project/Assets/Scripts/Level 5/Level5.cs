using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour {

    public static int socketCount;
	// Use this for initialization
	void Start () {
        //socketCount = 0;
	}

    public void IncrementFromSocket()
    {
        ++socketCount;
        Debug.Log(socketCount);
        if(socketCount >= 2)
        {
            MoveToPointArray.puzzleDone = true;
        }
    }
}
