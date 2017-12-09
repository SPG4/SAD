using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour {

    int totalAdded;

    void Start()
    {
        totalAdded = PlayerPrefs.GetFloat("Player1Score");
    }
    public void SavePlayerUse()
    {
        PlayerPrefs.SetFloat("Player1", totalAdded);
    }

    public void AddUseOfStandard(int add)
    {
        PlayerPrefs.GetFloat("PlayerSc0re", ++add);
    }
}
