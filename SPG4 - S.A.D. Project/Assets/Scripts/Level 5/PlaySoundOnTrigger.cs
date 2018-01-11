using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public string layerName;
    public bool noLayerCheck;
    public AudioClip sound;
    public float volume;
    public bool playSoundOnce;
    private AudioSource source;
    private bool played = false;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        if(noLayerCheck == true)
        {
            layerName = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (layerName != null)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(layerName))
            {
                if(playSoundOnce == false)
                {
                    source.PlayOneShot(sound, volume);
                }
                else if(played == false && playSoundOnce == true)
                {
                    played = true;
                    source.PlayOneShot(sound, volume);
                }
            }
        }
        else
        {
            if (playSoundOnce == false)
            {
                source.PlayOneShot(sound, volume);
            }
            else if (played == false && playSoundOnce == true)
            {
                played = true;
                source.PlayOneShot(sound, volume);
            }
        }
    }
}
