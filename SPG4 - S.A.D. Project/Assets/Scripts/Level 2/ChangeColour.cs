using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour {

    bool isGreen = false;
    public int nr;

    void Change2Red()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        isGreen = false;
        transform.root.SendMessage("CheckColor", nr);
    }

    void Change2Green()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        isGreen = true;
        transform.root.SendMessage("CheckColor", nr);
    }
}
