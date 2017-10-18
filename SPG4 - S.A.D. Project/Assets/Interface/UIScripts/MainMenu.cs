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
    public GameObject levelOneBtn;

    // Highlights
    public GameObject lineGame;
    public GameObject lineVideo;
    public GameObject lineControls;
    public GameObject lineKeyBindings;
    public GameObject lineMovement;
    public GameObject lineCombat;
    public GameObject lineGeneral;

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
        levelOneBtn.gameObject.SetActive(true);
    }

    public void DisableSelectLevel()
    {
        levelOneBtn.gameObject.SetActive(false);
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

        lineGame.gameObject.SetActive(true);
        lineControls.gameObject.SetActive(false);
        lineVideo.gameObject.SetActive(false);
        lineKeyBindings.gameObject.SetActive(false);
    }

    public void PlayHover()
    {
        hoverSound.GetComponent<AudioSource>();
        hoverSound.Play();
    }
    public void PlaySFXHover()
    {
        sfxhoversound.GetComponent<AudioSource>();
        sfxhoversound.Play();
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
}
