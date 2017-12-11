using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scream : MonoBehaviour {

    public AudioClip playSound;
    public float volume;
	// Use this for initialization
	void Start () {
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundLevelManager.Instance.PlaySingle(playSound);
        }
    }

    // Update is called once per frame

}
