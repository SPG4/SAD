using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelMusicControl : MonoBehaviour {
    public AudioMixerSnapshot outOfZone;
    public AudioMixerSnapshot inZone;
    public AudioClip[] stings;
    public AudioSource songIN;
    public AudioSource songOut;

    public float bpm = 130;

    private float m_TrainsitionIn;
    private float m_TrainsitionOut;
    private float m_QuaterNote;
    bool musicMain;
    private int count;
    
    void Start()
    {
        m_QuaterNote = 60 / bpm;
        m_TrainsitionIn = m_TrainsitionOut;
        m_TrainsitionOut = m_QuaterNote * 32;
    }

    private void FixedUpdate()
    {
        
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("TriggerZone"))
    //    {
    //        inZone.TransitionTo(m_TrainsitionIn);
    //    }
    //}
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("TriggerZone"))
    //    {
    //        outOfZone.TransitionTo(m_TrainsitionOut);
    //    }

    //}
    void OnTriggerEnter2D(Collider2D other)
    {
        ++count;
        musicMain = true;
        songIN.Stop();
        if (count == 1)
        {
        EnterZone();

        }
        if (other.CompareTag("TriggerZone"))
        {
            inZone.TransitionTo(m_TrainsitionIn);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        --count;
        musicMain = false;
        songOut.Stop();
        if(count == 0)
        {
        EnterZone();

        }
        if (other.CompareTag("TriggerZone"))
        {
            outOfZone.TransitionTo(m_TrainsitionOut);

        }
    }
    public void EnterZone()
    {
        if  (musicMain == true)
        {
            
            songOut.Play();
        }
        else
        {
            songIN.Play();
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "MusicPlayer")
    //    {

    //    }
    //}
}
