using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public string layerName;
    public bool noLayerCheck;
    public AudioClip sound;
    public float volume;
    private AudioSource source;

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
                source.PlayOneShot(sound, volume);
            }
        }
        else
        {
            source.PlayOneShot(sound, volume);
        }
    }
}
