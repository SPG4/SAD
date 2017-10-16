#pragma strict
import UnityEngine.UI;

// toggle buttons
var fullscreentext : GameObject;
var tooltipstext : GameObject;

// sliders
var musicSlider : GameObject;
var sfxSlider : GameObject;

private var sliderValue : float = 0.0;
private var sliderValueSFX : float = 0.0;

function Start () {
    // check difficulty
    //if(PlayerPrefs.GetInt("NormalDifficulty") == 1){
    //    //difficultynormaltext.GetComponent (Text).text = "NORMAL";
    //    difficultynormaltextLINE.gameObject.active = true;
    //    difficultyhardcoretextLINE.gameObject.active = false;
    //    //difficultyhardcoretext.GetComponent (Text).text = "hardcore";
    //}
    //else
    //{
    //    //difficultynormaltext.GetComponent (Text).text = "normal";
    //    //difficultyhardcoretext.GetComponent (Text).text = "HARDCORE";
    //    difficultyhardcoretextLINE.gameObject.active = true;
    //    difficultynormaltextLINE.gameObject.active = false;

    }

    // check slider values
    musicSlider.GetComponent.<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
    sfxSlider.GetComponent.<Slider>().value = PlayerPrefs.GetFloat("SFXVolume");

    //// check full screen
    //if(Screen.fullScreen == true){
    //    fullscreentext.GetComponent (Text).text = "on";
    //}
    //else if(Screen.fullScreen == false){
    //    fullscreentext.GetComponent (Text).text = "off";
    //}

    //// check hud value
    //if(PlayerPrefs.GetInt("ShowHUD")==0){
    //    showhudtext.GetComponent (Text).text = "off";
    //}
    //else{
    //    showhudtext.GetComponent (Text).text = "on";
    //}

    // check tool tip value
    if(PlayerPrefs.GetInt("ToolTips")==0){
        tooltipstext.GetComponent (Text).text = "off";
    }
    else{
        tooltipstext.GetComponent (Text).text = "on";
    }


function Update(){
	sliderValue = musicSlider.GetComponent.<Slider>().value;
	sliderValueSFX = sfxSlider.GetComponent.<Slider>().value;
}


function MusicSlider(){
	PlayerPrefs.SetFloat("MusicVolume", sliderValue);
}

function SFXSlider(){
	PlayerPrefs.SetFloat("SFXVolume", sliderValueSFX);
}


// show tool tips like: 'How to Play' control pop ups
function ToolTips(){
	if(PlayerPrefs.GetInt("ToolTips")==0){
		PlayerPrefs.SetInt("ToolTips",1);
		tooltipstext.GetComponent (Text).text = "on";
	}
	else if(PlayerPrefs.GetInt("ToolTips")==1){
		PlayerPrefs.SetInt("ToolTips",0);
		tooltipstext.GetComponent (Text).text = "off";
	}
}
