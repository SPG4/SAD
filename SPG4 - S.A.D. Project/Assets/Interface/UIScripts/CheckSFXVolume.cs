using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSFXVolume : MonoBehaviour {

    void Start()
    {
        // remember volume level from last time
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVolume();
    }
    public void UpdateVolume()
    {
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume");
    }
}
