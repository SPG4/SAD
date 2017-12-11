using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

    public Animator CameraObject;
    public GameObject PanelGame;
    public AudioSource hoverSound;
    public AudioSource sfxhoversound;
    public AudioSource clickSound;
    public GameObject areYouSure;

    // Campaign button sub menu
    public GameObject continueBtn;
    public GameObject newGameBtn;
    public GameObject loadGameBtn;

    // Level Menu Buttons
    public GameObject level1, level2, level3, level4, level5, level6;

    public GameObject[] levelbuttons;

    public int completedLevels;

    public void Start()
    {
        levelbuttons = new GameObject[6];
        levelbuttons[0] = level1;
        levelbuttons[1] = level2;
        levelbuttons[2] = level3;
        levelbuttons[3] = level4;
        levelbuttons[4] = level5;
        levelbuttons[5] = level6;
    }
    public void PlayCampaign()
    {
        areYouSure.gameObject.SetActive(false);
        continueBtn.gameObject.SetActive(true);
        newGameBtn.gameObject.SetActive(true);
        loadGameBtn.gameObject.SetActive(true);
    }
	public void DisablePlayCampaign()
    {
        continueBtn.gameObject.SetActive(false);
        newGameBtn.gameObject.SetActive(false);
        loadGameBtn.gameObject.SetActive(false);
        DisableSelectLevel();
    }

    public void SelectLevel()
    {
        for (int i = 0; i < completedLevels; i++)
        {
            levelbuttons[i].gameObject.SetActive(true);
        }
        
    }

    public void DisableSelectLevel()
    {
        for (int i = 0; i < completedLevels; i++)
        {
            levelbuttons[i].gameObject.SetActive(false);
        }
    }

    public void Position2()
    {
        DisablePlayCampaign();
        CameraObject.SetFloat("Animate", 1);
    }

    public void Position1()
    {
        CameraObject.SetFloat("Animate", 0);
    }

    public void GamePanel()
    {
        PanelGame.gameObject.SetActive(true);
    }

    public void PlayHover()
    {
        hoverSound.GetComponent<AudioSource>();
        hoverSound.Play();
    }

    public void PlayClick()
    {
        clickSound.GetComponent<AudioSource>();
        clickSound.Play();
    }

    public void AreYouSure()
    {
        areYouSure.gameObject.SetActive(true);
        DisablePlayCampaign();
    }

    public void No()
    {
        areYouSure.gameObject.SetActive(false);
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void continueLastPlayedScene()
    {
        LoadScene(PlayerPrefs.GetInt("CurrentScene"));
    }
}
