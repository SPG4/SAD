using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour {

    int totalAdded;

    void Start()
    {
        totalAdded = PlayerPrefs.GetInt("Player1Score");
    }
    public void SavePlayerUse()
    {
        PlayerPrefs.SetInt("Player1Score", totalAdded);
    }

    public void AddUseOfStandard(int add)
    {
        PlayerPrefs.GetInt("Player1Score", ++add);
    }
}
