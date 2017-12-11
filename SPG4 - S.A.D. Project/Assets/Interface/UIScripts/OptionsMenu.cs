using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    
    public GameObject musicSlider;
    public GameObject sfxSlider;

    public AudioClip sfxTick;

    private float sliderValue = 0;
    private float sliderValueSFX = 0;
    
	void Start () {
        musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFXVolume");
	}
	
	void Update () {
        sliderValue = musicSlider.GetComponent<Slider>().value;
        sliderValueSFX = sfxSlider.GetComponent<Slider>().value;
	}

    public void MusicSlider()
    {
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SFXSlider()
    {
        PlayerPrefs.SetFloat("SFXVolume", sliderValueSFX);
        SoundLevelManager.Instance.PlaySingle(sfxTick);
    }

}
