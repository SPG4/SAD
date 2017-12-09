using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckMusicVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // remember volume level from last time
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
    }
	
	// Update is called once per frame
	void Update () {
        UpdateVolume();

    }
    public void UpdateVolume()
    {
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}
