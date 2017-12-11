using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundLevelManager : MonoBehaviour {

    public AudioSource sfxSource;
    public AudioSource musicSource;

    private AudioClip[] musicClips;

    public AudioClip menuMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip level3Music;
    public AudioClip level4Music;
    public AudioClip level5Music;
    public AudioClip bossMusic;

    public float musicVolume;
    public float sfxVolume;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    public static SoundLevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            musicClips = new AudioClip[7];
            musicClips[0] = menuMusic;
            musicClips[1] = level1Music;
            musicClips[2] = level2Music;
            musicClips[3] = level3Music;
            musicClips[4] = level4Music;
            musicClips[5] = level5Music;
            musicClips[6] = bossMusic;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        

        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume");

        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;

    }

    public void Update()
    {
        UpdateMusicVolume();
        UpdateSFXVolume();
    }

    public void UpdateMusicVolume()
    {
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void UpdateSFXVolume()
    {
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void PlaySingle(AudioClip clip)
    {
        //Debug.Log("played" + clip);
        sfxSource.clip = clip;

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source ot the randmly chosen pitch.
        sfxSource.pitch = randomPitch;

        sfxSource.Play();
    }
    public void UpdateMusic()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        musicSource.clip = musicClips[scene];
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        UpdateMusic();
        if (!musicSource.isPlaying)
            musicSource.Play();
    }
}
