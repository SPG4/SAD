using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelMusicControl : MonoBehaviour {
    public AudioMixerSnapshot outOfZone;
    public AudioMixerSnapshot inZone;
    public AudioClip[] stings;
    public AudioSource stingSource;
    public float bpm = 130;

    private float m_TrainsitionIn;
    private float m_TrainsitionOut;
    private float m_QuaterNote;

    void Start()
    {
        m_QuaterNote = 60 / bpm;
        m_TrainsitionIn = m_TrainsitionOut;
        m_TrainsitionOut = m_QuaterNote * 32;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("TriggerZone"))
    //    {
    //        inZone.TransitionTo(m_TrainsitionIn);
    //    }
    //}
    //    void OnTriggerExit(Collider other)
    //    {
    //    if (other.CompareTag("TriggerZone"))
    //    {
    //        outOfZone.TransitionTo(m_TrainsitionOut);
    //    }

    //}
    // void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("TriggerZone"))
    //    {
    //        inZone.TransitionTo(m_TrainsitionIn);
    //    }
    //}

    // void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("TriggerZone"))
    //    {
    //        outOfZone.TransitionTo(m_TrainsitionOut);
    //    }
    //}

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "MusicPlayer")
        {
            
        }
    }
}
