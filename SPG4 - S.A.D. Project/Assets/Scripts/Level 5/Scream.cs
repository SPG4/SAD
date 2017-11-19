using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scream : MonoBehaviour {

    public AudioClip playSound;
    public float volume;
    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Enter");
            
            audioSource.PlayOneShot(playSound, 0.7f);
        }
    }

    // Update is called once per frame

}
