using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl5Puzzle3Done : MonoBehaviour
{ 
    public AudioClip sound;
    public float volume;
    private AudioSource source;
    private bool done = false;
    private bool done2 = false;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(done == false && Level5.socketCount == 2)
        {
            if (Level5.socketCount == 2)
            {
                source.PlayOneShot(sound, volume);
            }
            done = true;
        }
        if(done2 == false && Lvl5Puzzel2.pzl2Done == true)
        {
            source.PlayOneShot(sound, volume);
            done2 = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
