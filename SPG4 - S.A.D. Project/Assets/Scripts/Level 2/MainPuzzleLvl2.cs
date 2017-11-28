using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPuzzleLvl2 : MonoBehaviour {

    public GameObject bridge;
    public bool slot1, slot2, slot3;
    bool canBreak = true;
	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (slot1 && slot2 && slot3 && canBreak)
        {
            bridge.GetComponent<HingeJoint2D>().breakForce = 1;
            canBreak = false;
        }
	}

    void PieceCorrect(int slot)
    {
        if (slot == 1)
            slot1 = true;
        if (slot == 2)
            slot2 = true;
        if (slot == 3)
            slot3 = true;
    }

    void PieceIncorrect(int slot)
    {
        if (slot == 1)
            slot1 = false;
        if (slot == 2)
            slot2 = false;
        if (slot == 3)
            slot3 = false;
    }
}
