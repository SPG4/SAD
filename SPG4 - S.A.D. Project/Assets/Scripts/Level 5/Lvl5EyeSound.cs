using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl5EyeSound : MonoBehaviour {
    public AudioClip playSound;
    public float volume;
    private AudioSource audioSource;
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Eye"))
        {
            audioSource.PlayOneShot(playSound, 0.7f);
        }
    }
}
